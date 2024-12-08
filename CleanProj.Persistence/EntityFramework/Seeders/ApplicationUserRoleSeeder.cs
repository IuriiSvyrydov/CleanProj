using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Seeders;

public sealed class ApplicationUserRoleSeeder : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasData(
            new ApplicationUserRole
            {
                UserId = new Guid("0B9BB71A-FEB6-45CC-9784-7401D8AE85B8"),
                RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC") // Admin role
            }
        );
    }
}
