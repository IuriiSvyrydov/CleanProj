using CleanProj.Domain.Common;
using CleanProj.Domain.Entities;
using CleanProj.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CleanProj.Domain.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>,ICreatedByEntity, IModifiedByEntity
{
    public FullName FullName { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public ICollection<UserSocialMediaAccount> UserSocialMediaAccounts { get; set; } = [];
    public ICollection<UserFavoritePrompt> UserFavoritePrompts { get; set; } = [];
    public ICollection<UserLikePrompt> UserLikePrompts { get; set; } = [];
    public ICollection<UserPromptComment> UserPromptComments { get; set; } = [];

}