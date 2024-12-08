using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace CleanProj.API.Filters;

public sealed class SwaggerJsonIgnoreFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        List<PropertyInfo> properties = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties())
            .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
            .ToList();
        if (!properties.Any())
        {
            return;
        }

        foreach (var property in properties)
        {
            operation.Parameters = operation.Parameters
                .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                .ToList();
        }
    }
}