
using CleanProj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProj.Persistence.EntityFramework.Configurations
{
    public class UserLikePromptConfiguration : IEntityTypeConfiguration<UserLikePrompt>
    {
        public void Configure(EntityTypeBuilder<UserLikePrompt> builder)
        {
            builder.ToTable("user_like_prompt");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Prompt)
            .WithMany(x => x.UserLikePrompts)
            .HasForeignKey(x => x.PromptId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
            .WithMany(x => x.UserLikePrompts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}