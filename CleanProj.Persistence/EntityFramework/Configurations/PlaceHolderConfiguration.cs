using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class PlaceHolderConfiguration : IEntityTypeConfiguration<PlaceHolder>
{
    public void Configure(EntityTypeBuilder<PlaceHolder> builder)
    {
        builder.ToTable("placeHolders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasOne(x => x.Prompt)
            .WithMany(x => x.PlaceHolders)
            .HasForeignKey(x => x.PropmptId)
            .IsRequired();
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x=>x.CreatedByUserId)
            .IsRequired(false);
    }
}