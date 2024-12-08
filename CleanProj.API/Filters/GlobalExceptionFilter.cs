
using CleanProj.API.Helpers;

using CleanProj.Domain.ValueObjects;
using FluentValidation;

using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanProj.API.Filters;
public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);
        context.ExceptionHandled = true;
        if (context.Exception is ValidationException validationException)
        {
            var responseMessage = MessageHelper.GeneralValidationErrorMessage;
             var errors = validationException.Errors
        .GroupBy(x => x.PropertyName)
        .Select(g => new ValidationError(g.Key,
            string.Join(", ", g.Select(x => x.ErrorMessage)))
        ).ToList();
                 //   context.Result = new BadRequestObjectResult(ResponseDto<object>.Error(re));
       

        }
    }
}