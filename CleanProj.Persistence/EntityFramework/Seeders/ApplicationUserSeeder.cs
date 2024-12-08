using CleanProj.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanProj.Persistence.EntityFramework.Seeders
{
    public sealed class ApplicationUserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var createUserId = new Guid("0B9BB71A-FEB6-45CC-9784-7401D8AE85B8");
            var user = new ApplicationUser
            {
                Id = createUserId,
                UserName = "ferro5",
                NormalizedUserName = "FERRO5",
                Email = "sviridov288@gmail.com",
                NormalizedEmail = "SVIRIDOV288@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "0B9BB71A-FEB6-45CC-9784-7401D8AE85B9",
                ConcurrencyStamp = "0B9BB71A-FEB6-45CC-9784-7401D8AE85B4",
                CreateAt = DateTimeOffset.UtcNow,
                CreatedByUserId = createUserId.ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, "test123");
            
            builder.HasData(user);
        }
    }
}