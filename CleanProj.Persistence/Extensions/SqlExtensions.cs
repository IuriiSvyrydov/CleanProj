
using CleanProj.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace CleanProj.Persistence.Extensions
{
    public static class SqlExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(config=>config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        
    }
}