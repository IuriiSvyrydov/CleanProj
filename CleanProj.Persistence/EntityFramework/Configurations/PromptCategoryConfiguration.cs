using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class PromptCategoryConfiguration:IEntityTypeConfiguration<PromptCategory>
{
    public void Configure(EntityTypeBuilder<PromptCategory> builder)
    {
        builder.ToTable("prompt_categories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasOne(x=>x.Prompt).WithMany(x=>x.PromptCategories)
            .HasForeignKey(x=>x.PromptId)
            .IsRequired();
        builder.HasOne(x=>x.Category).WithMany(x=>x.PromptCategories)
            .HasForeignKey(x=>x.CategoryId)
            .IsRequired();
        builder.HasIndex(x => new { x.PromptId, x.CategoryId })
            .IsUnique();
        builder.Property(x=>x.CreatedByUserId).IsRequired(false);
    }
}