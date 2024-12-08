using CleanProj.API.Models;
using CleanProj.Domain.Entities;
using MediatR;

namespace CleanProj.API.V1.Categories.Create;

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest<ResponseDto<Guid>>;