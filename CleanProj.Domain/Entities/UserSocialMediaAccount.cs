using CleanProj.Domain.Common;
using CleanProj.Domain.Enums;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

public sealed class UserSocialMediaAccount:BaseEntity
{
    public SocialMediaType SocialMediaType { get; set; }
    public string Url { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
}