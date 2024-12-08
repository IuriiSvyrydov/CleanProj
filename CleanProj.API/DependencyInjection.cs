using System.Reflection;
using CleanProj.API.Configuration;
using CleanProj.API.Services;
using CleanProj.API.V1.Prompts.Validators;
using CleanProj.Domain.Identity;
using CleanProj.Domain.Settings;
using CleanProj.Persistence.EntityFramework.Contexts;
using CleanProj.Persistence.EntityFramework.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace CleanProj.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services,IConfiguration configuration)
        {
            ////builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["AzureKeyVaultSettings:Uri"]),
            ////    new ClientSecretCredential(
            ////        tenantId: builder.Configuration["AzureKeyVaultSettings:TenantId"],
            ////        clientId: builder.Configuration["AzureKeyVaultSettings:ClientId"],
            ////        clientSecret: builder.Configuration["AzureKeyVaultSettings:ClientSecret"]));
 
            services.AddMemoryCache();
            services.AddSwaggerConfiguration();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddScoped<ICurrentUserService, GetCurrentUserManager>();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.Configure<CloudFlareR2Settings>(configuration.GetSection(nameof(CloudFlareR2Settings)));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
