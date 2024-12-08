using CleanProj.API.Helpers;
using CleanProj.API.Models;
using CleanProj.Domain.Entities;
using CleanProj.Persistence.EntityFramework.Contexts;
using MediatR;

namespace CleanProj.API.V1.Categories.Create;

public sealed class CreateCategoryCommandHandler:IRequestHandler<CreateCategoryCommand,ResponseDto<Guid>>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateCategoryCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, request.Description);
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return ResponseDto<Guid>.Success(category.Id, MessageHelper.GetApiSuccessCreatedMessage(("category")));
    }
}