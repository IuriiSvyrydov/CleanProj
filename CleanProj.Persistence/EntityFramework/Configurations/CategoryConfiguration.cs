using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        builder.Property(x=>x.Name).IsRequired()
            .HasMaxLength(100);
        builder.HasMany(x => x.PromptCategories)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
        builder.Property(x => x.CreatedByUserId).IsRequired(false);
        builder.Property(x=>x.Description).IsRequired()
            .HasMaxLength(500);
        builder.Property(x=>x.ModifiedByUserId).IsRequired(false);
        builder.Property(x=>x.ModifiedAt).IsRequired(false);
    }
}