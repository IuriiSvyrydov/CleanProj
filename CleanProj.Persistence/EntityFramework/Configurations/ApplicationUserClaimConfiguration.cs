using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.ToTable("application_user_claims");
    }
}