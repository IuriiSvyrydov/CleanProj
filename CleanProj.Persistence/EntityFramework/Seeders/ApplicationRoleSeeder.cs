using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Seeders;

public sealed class ApplicationRoleSeeder: IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole
            {
                Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "8d04dce2-969a-435d-bba4-df3f325983dc"
            },
            new ApplicationRole
            {
                Id = new Guid("CFD242D3-2107-4563-B2A4-15383E683964"),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "cfd242d3-2107-4563-b2a4-15383e683964"
            }
        );
    }
}