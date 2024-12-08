using CleanProj.API.Helpers;
using CleanProj.API.Models;
using CleanProj.Domain.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanProj.API.Filters;

public sealed class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var validationErrors = context.ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .Select(x => new ValidationError(x.Key,
                    x.Value!.Errors.First()
                    .ErrorMessage))
                .ToList();
            var response = ResponseDto<object>.Error(
              MessageHelper.GeneralValidationErrorMessage, validationErrors);
            context.Result = new BadRequestObjectResult(response);
            return;
        }
        await next();

    }
}