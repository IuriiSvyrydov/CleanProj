
using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public class ApplicationUserConfiguration:IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x=>x.PhoneNumber).IsRequired(false);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.HasIndex(x=>x.Email).IsUnique();
      builder.OwnsOne(x => x.FullName, fullname =>
    {
        fullname.Property(x => x.FirstName).HasMaxLength(100);
        fullname.Property(x => x.LastName).HasMaxLength(100);
    });
    }
}