
using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("application_user_role");
        builder.HasKey(x=>new{x.UserId,x.RoleId});
    }
}