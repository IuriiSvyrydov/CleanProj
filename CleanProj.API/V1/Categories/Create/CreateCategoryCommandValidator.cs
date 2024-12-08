using System.Data.Entity;
using CleanProj.Persistence.EntityFramework.Contexts;
using FluentValidation;

namespace CleanProj.API.V1.Categories.Create;

public sealed class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    private readonly ApplicationDbContext _context;
    public CreateCategoryCommandValidator(ApplicationDbContext context)
    {
        _context = context;
    }
    public CreateCategoryCommandValidator()
    {
        RuleFor(x=>x.Name)
        .MustAsync(BeUniqueNameAsync)
            .NotEmpty()
            .MaximumLength(100)           
            .WithMessage("Category name must be between 3 and 100 characters")
            .MinimumLength(2)
            .WithMessage("Category name must be between 2 and 100 characters");
        RuleFor(x=>x.Description)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Description must be between 0 and 500 characters")
            .MinimumLength(2)
            .WithMessage("Description must be between 2 and 500 characters");
    }

    private async Task<bool> BeUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
         return !await _context
         .Categories.AnyAsync(x=>string.Equals(x.Name,name,StringComparison.OrdinalIgnoreCase),cancellationToken);
    }
}