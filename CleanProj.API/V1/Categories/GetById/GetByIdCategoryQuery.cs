using MediatR;

namespace CleanProj.API.V1.Categories.GetById;

public record GetByIdCategoryQuery(Guid Id) : IRequest<GetCategoryByIdDTO>, ICacheable
{
    public string CacheKey =>CacheKeyHelper.GetByIdCategoryKey(Id);
};