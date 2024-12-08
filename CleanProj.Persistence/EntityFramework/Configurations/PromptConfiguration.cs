using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class PromptConfiguration:IEntityTypeConfiguration<Prompt>
{


    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.ToTable("prompts");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();        
    builder.Property(x=>x.IsActive)
            .HasDefaultValue(false)
            .IsRequired();
    builder.HasMany(x => x.UserFavoritePrompts)
            .WithOne(x => x.Prompt)
            .HasForeignKey(x => x.PromptId);
     builder.HasMany(x => x.PromptCategories)
            .WithOne(x => x.Prompt)
            .HasForeignKey(x =>x.PromptId);
    builder.HasMany(x=>x.UserLikePrompts)
            .WithOne(x => x.Prompt)
            .HasForeignKey(x => x.PromptId);

    builder.Property(x=>x.Description)
        .HasMaxLength(1000)
        .IsRequired();
    builder.Property(x=>x.Title)
        .IsRequired()
        .HasMaxLength(200);
    builder.Property(x=>x.ImageUrl)
        .IsRequired(false);   

    }
}