using Asp.Versioning;
using CleanProj.API.Helpers;
using CleanProj.API.Models;
using CleanProj.API.V1.Prompts;
using CleanProj.Domain.Entities;
using CleanProj.Persistence.EntityFramework.Contexts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CleanProj.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PromptsController : ControllerBase
    {
        #region PRIVATE FIELDS

        private readonly IMemoryCache _memoryCache;
        private const string AllPromptCacheKey = "all-prompts";
        private const string PromptKeyCachePrefix = "prompt-";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<CreatePromptDTO> _createvVlidator;
        private readonly IValidator<UpdatePromptDTO> _updatevlidator;

        #endregion

        public PromptsController(IMemoryCache memoryCache, ApplicationDbContext dbContext, IValidator<CreatePromptDTO> createvVlidator,
            IValidator<UpdatePromptDTO> updatevlidator)
        {
            _memoryCache = memoryCache;
            _dbContext = dbContext;
            _createvVlidator = createvVlidator;
            _updatevlidator = updatevlidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(AllPromptCacheKey, out List<GetAllPromptDto> cachedPrompts))
                return Ok(cachedPrompts);
            var prompts = await _dbContext.Prompts
                .AsNoTracking()
                .Select(prompt => new GetAllPromptDto(
                    prompt.Id,
                    prompt.Title,
                    prompt.Description,
                    prompt.ImageUrl,
                    prompt.IsActive))
                .ToListAsync(cancellationToken);
            _memoryCache.Set(AllPromptCacheKey, prompts, CacheDuration);
            return Ok(prompts);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = $"{PromptKeyCachePrefix}{id}";
            if (_memoryCache.TryGetValue(cacheKey, out GetPromptByIdDTO cachedPrompt))
                return Ok(cachedPrompt);

            var prompt = await _dbContext.Prompts
                .AsNoTracking()
                .Include(p => p.PromptCategories)
                .ThenInclude(p => p.Category)
                .Include(p => p.PlaceHolders)
                .Where(z => z.Id == id)
                .Select(p => new GetPromptByIdDTO(
                    p.Id,
                    p.Title,
                    p.Description,
                    p.Content,
                    p.ImageUrl,
                    p.IsActive,
                    p.PromptCategories.Select(c => new PromptCategoryDto(c.Category.Id, c.Category.Name)).ToList(),
                    p.PlaceHolders.Select(c => new PlaceHolderDto(c.Id, c.Name)).ToList()))
                .FirstOrDefaultAsync(cancellationToken);

            if (prompt is null)
                return NotFound();
            _memoryCache.Set(cacheKey, prompt, CacheDuration);
            return Ok(prompt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePromptDTO promptDto, CancellationToken cancellationToken)
        {
            var validationResult = await _createvVlidator.ValidateAsync(promptDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                
                var prompt = Prompt.Create(promptDto.Title, promptDto.Description, promptDto.Content,
                    promptDto.IsActive);

                _dbContext.Prompts.Add(prompt);
                    var promptCategories = promptDto.CategoryIds
                        .Select(categoryId => PromptCategory.Create(prompt.Id, categoryId));
                    _dbContext.PromptCategories.AddRange(promptCategories);

                if (promptDto.PlaceHoldersName is not null && promptDto.PlaceHoldersName.Any())
                {
                    var placeHolders = promptDto.PlaceHoldersName
                        .Select(name => PlaceHolder.Create(name, prompt.Id));
                    _dbContext.PlaceHolders.AddRange(placeHolders);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                InvalidateCache();
                return Ok(ResponseDto<Guid>.Success(prompt.Id,
                    MessageHelper.GetApiSuccessCreatedMessage("Successfully created prompt")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePromptDTO promptDto,
            CancellationToken cancellationToken)
        {
            var validationResult = await _updatevlidator.ValidateAsync(promptDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var prompt = await _dbContext.Prompts
                .Include(x => x.PromptCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.PlaceHolders)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (prompt is null)
                return NotFound();
            prompt.Update(promptDto.Title, promptDto.Description, promptDto.Content, promptDto.IsActive);
            prompt.PromptCategories.Clear();
            var categories = await _dbContext.Categories
                .Where(c => promptDto.CategoryIds.Contains(c.Id))
                .ToListAsync(cancellationToken);
            foreach (var category in categories)
            {
                prompt.PromptCategories.Add(new PromptCategory
                {
                    Category = category
                });
              
            }

            prompt.PlaceHolders.Clear();
            foreach (var placeHolderName in promptDto.PlaceHoldersName)
            {
                prompt.PlaceHolders.Add(new PlaceHolder { Name = placeHolderName });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            InvalidateCache(id);

            return Ok(ResponseDto<Guid>.Success(MessageHelper.GetApiSuccessCreatedMessage("Prompt updated successfully")));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _dbContext
                .Prompts
                .Where(prompt => prompt.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
            if (result==0)
                return NotFound();
            InvalidateCache(id);
            return Ok(ResponseDto<Guid>.Success(MessageHelper.GetApiSuccessDeletedMessage("Prompt deleted successfully")));
        }

        private void InvalidateCache(Guid? categoryId = null)
        {
            _memoryCache.Remove(AllPromptCacheKey);
            if (categoryId.HasValue)
                _memoryCache.Remove($"{AllPromptCacheKey}{categoryId}");
        }
    }
}

