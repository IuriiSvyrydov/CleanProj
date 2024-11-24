using System.Reflection;
using CleanProj.Domain.Entities;
using CleanProj.Domain.Identity;
using CleanProj.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanProj.Persistence.EntityFramework.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    public DbSet<PlaceHolder> PlaceHolders { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Prompt> Prompts { get; set; } = null!;
    public DbSet<PromptCategory> PromptCategories { get; set; } = null!;
    public DbSet<UserSocialMediaAccount> UserSocialMediaAccounts { get; set; } = null!;
    public DbSet<UserPromptComment> UserPromptComments { get; set; } = null!;
    public DbSet<UserFavoritePrompt> UserFavoritePrompts { get; set; } = null!;
    public DbSet<UserLikePrompt> UserLikePrompts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ToLowerCaseNames();
        base.OnModelCreating(builder);
    }
}