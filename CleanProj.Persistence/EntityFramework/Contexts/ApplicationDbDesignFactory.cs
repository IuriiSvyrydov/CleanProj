using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanProj.Persistence.EntityFramework.Contexts;

public class ApplicationDbDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Get the configuration from appsettings.json

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=REVISION-PC;Database=CleanDb;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=true;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}