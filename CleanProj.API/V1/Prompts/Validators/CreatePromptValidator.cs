using System.Data.Entity;
using CleanProj.Persistence.EntityFramework.Contexts;
using FluentValidation;

namespace CleanProj.API.V1.Prompts.Validators;

public class CreatePromptValidator : AbstractValidator<CreatePromptDTO>
{
    private readonly ApplicationDbContext _context;
    public CreatePromptValidator(ApplicationDbContext context)
    {
        _context = context;
        RuleFor(x=>x.Title).NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters")
            .MinimumLength(3)
            .WithMessage("Title must not exceed 3 characters");

        RuleFor(x => x.CategoryIds)
            .NotEmpty()
            .WithMessage("CategoryIds is required")
            .Must(BeAValidCategoryId);
        RuleFor(x => x.IsActive)
            .NotEmpty()
            .WithMessage("IsActive is required");
        
        RuleFor(x=>x.CategoryIds)
            .NotEmpty()
            .WithMessage("CategoryIds is required")
            .MustAsync(CategoryIdsExists)
            .WithMessage("CategoryIds must not exceed 3 characters");

        RuleFor(x=>x.Image)
        .Must(BeValidImage)
        .When(x=>x.Image is not null)
        .WithMessage("Image is required");
        RuleFor(x=>x.PlaceHoldersName)
            .Must(x=>x==null|| x.Count==0)
            .WithMessage("PlaceHoldersName is required");
    }

    private bool BeAValidCategoryId(List<Guid> categories)
    {
        return categories.Count > 0;
    }
    private bool BeValidImage(IFormFile? image)
    {
        if(image is null) return false;
        var allowedExtensions = new  []{".png", ".jpg","jpeg",".webp"};
        var extension  = Path.GetExtension(image.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }

    private  Task<bool> CategoryIdsExists(List<Guid> categories,CancellationToken cancellationToken)
    {
        return  _context.Categories.AnyAsync(x=>categories.Contains(x.Id));
    }

    private bool BeValidPlaceHoldersName(List<string>? placeHolderNames)
    {
        return placeHolderNames==null || placeHolderNames.Count == 0;
    }
}