using System.IO.Compression;
using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations;

public sealed class UserPromptCommentConfiguration: IEntityTypeConfiguration<UserPromptComment>
{
    public void Configure(EntityTypeBuilder<UserPromptComment> builder)
    {
        builder.ToTable("user_prompt_comment");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(1000);
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserPromptComments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.ParentComment)
            .WithMany(x=>x.ChildComments)
            .HasForeignKey(x=>x.ParentCommentId)
            .IsRequired(false);
        builder.HasOne(x => x.Prompt)
            .WithMany(x => x.UserPromptComments)
            .HasForeignKey(x => x.PromptId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x=>x.CreatedByUserId)
            .IsRequired(false);
        builder.Property(x=>x.ModifiedByUserId)
            .IsRequired(false);
    }
}