using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class UserFavoritePromptConfiguration: IEntityTypeConfiguration<UserFavoritePrompt>
{
    public void Configure(EntityTypeBuilder<UserFavoritePrompt> builder)
    {
        builder.ToTable("user_favorite_prompts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        builder.HasOne(x => x.Prompt)
            .WithMany(x => x.UserFavoritePrompts)
            .HasForeignKey(x => x.PromptId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserFavoritePrompts)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x=>new { x.UserId,x.PromptId }).IsUnique();
    }
}