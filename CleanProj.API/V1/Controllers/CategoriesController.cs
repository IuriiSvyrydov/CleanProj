
using Asp.Versioning;
using CleanProj.API.Helpers;
using CleanProj.API.Models;
using CleanProj.API.V1.Categories;
using CleanProj.Domain.Entities;
using CleanProj.Persistence.EntityFramework.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace CleanProj.API.V1.Controllers;

[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private const string AllCategoriesCacheKey = "all-categories";
    private const string CategoryKeyCachePrefix = "category-";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
    private readonly ApplicationDbContext _dbContext;

    public CategoriesController(IMemoryCache memoryCache, ApplicationDbContext dbContext)
    {
        _memoryCache = memoryCache;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync(CancellationToken cancellationToken)

    {
        if (_memoryCache.TryGetValue(AllCategoriesCacheKey, out List<GetAllCategoriesDTO> cachedCategories))
        {
            return Ok(cachedCategories);
        }
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Select(category => new GetAllCategoriesDTO(category.Id, category.Name))
            .ToListAsync();
        var cacheOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

        _memoryCache.Set(AllCategoriesCacheKey, categories, cacheOptions);

        return Ok(categories);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"{AllCategoriesCacheKey}{id}";
        if (_memoryCache.TryGetValue(cacheKey, out GetAllCategoriesDTO cachedCategories))
        {
            return Ok(cachedCategories);
        }
        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(category => new GetCategoryByIdDTO(category.Id, category.Name, category.Description))
            .FirstOrDefaultAsync(cancellationToken);
        if (category is null)
        {
            return NotFound();
        }
        var cacheOptions = new MemoryCacheEntryOptions()
    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

        _memoryCache.Set(cacheKey, category, cacheOptions);

        return Ok(category);
    }

    [HttpPost("/api/categories")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDTO category,
        CancellationToken cancellationToken)
    {
        var newCategory = Category.Create(category.Name, category.Description);
        _dbContext.Categories.Add(newCategory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        //return Created("", new GetCategoryByIdDTO(newCategory.Id, newCategory.Name, newCategory.Description));
        return Ok( ResponseDto<Guid>.Success(newCategory.Id,MessageHelper.GetApiSuccessCreatedMessage("Category created")));
    }

    [HttpPut("/api/categories/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCategoryDTO dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest();
        var category = await _dbContext.Categories
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (category == null)
            return NotFound();
        category.Name = dto.Name;
        category.Description = dto.Description;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(ResponseDto<Guid>.Success(category.Id,MessageHelper.GetApiSuccessUpdatedMessage("Category updated")));
    }

    [HttpDelete("/api/categories/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
            return NotFound();
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        InvalidateCache(id);
        return Ok(ResponseDto<Guid>.Success(MessageHelper.GetApiSuccessDeletedMessage("Category deleted")));
    }

    private void InvalidateCache(Guid? categoryId = null)
    {
        _memoryCache.Remove(AllCategoriesCacheKey);
        if (categoryId.HasValue)
            _memoryCache.Remove($"{AllCategoriesCacheKey}{categoryId}");
    }

}
