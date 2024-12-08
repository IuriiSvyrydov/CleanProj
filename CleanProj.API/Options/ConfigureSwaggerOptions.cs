using Asp.Versioning.ApiExplorer;
using CleanProj.API.Filters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanProj.API.Options;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.OperationFilter<SwaggerJsonIgnoreFilter>();
        string folder = AppContext.BaseDirectory;
        if (Directory.Exists(folder))
        {
            foreach (string record in Directory.GetFiles(folder, "*.xml", SearchOption.AllDirectories))
            {
                options.IncludeXmlComments(record);
            }
        }
        options.ExampleFilters();
        Configure(options);
    }
    public void Configure(string? name, SwaggerGenOptions options)
    {
        options.OperationFilter<SwaggerJsonIgnoreFilter>();
        //string folder = Excutable
    }
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        OpenApiInfo info = new()
        {
            Title = "CleanProj API",
            Description = "CleanProj API",
            Version = description.ApiVersion.ToString(),
            Contact = new()
            {
                Name = "Clean Proj",
                Email = "sviridov288@gmail.com",
                Url = new("https://github.com/sviridov288")
            }
        };
        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }
        return info;
    }

}