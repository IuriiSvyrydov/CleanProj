using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CleanProj.API.Options;
using Swashbuckle.AspNetCore.Filters;

namespace CleanProj.API.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen();
       // ... existing code ...
        services.AddApiVersioning(static options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerExamplesFromAssemblyOf<Program>();
        
        return services;
    }
    public static IApplicationBuilder UseSwaggerWithVersion( this IApplicationBuilder app)
     
{
 var versionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
    
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Source API {description.GroupName}");
        }
    });
    
    return app;
}

    }
    
