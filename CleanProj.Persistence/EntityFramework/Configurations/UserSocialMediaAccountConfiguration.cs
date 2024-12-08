using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class UserSocialMediaAccountConfiguration: IEntityTypeConfiguration<UserSocialMediaAccount>
{
    public void Configure(EntityTypeBuilder<UserSocialMediaAccount> builder)
    {
        builder.ToTable("user_social_media_accounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
        
        builder.HasOne(x=>x.User)
            .WithMany(x=>x.UserSocialMediaAccounts)
            .HasForeignKey(x=>x.UserId)
            .IsRequired();
        builder.Property(x=>x.SocialMediaType)
        .HasConversion<string>()
        .IsRequired()
        .HasMaxLength(50);
        
        builder.HasIndex(x=>new { x.UserId, x.SocialMediaType }).IsUnique();
        builder.Property(x=>x.ModifiedByUserId)
            .IsRequired(false);
        builder.Property(x=>x.CreatedByUserId)
            .IsRequired(false);
        
        
    }
}