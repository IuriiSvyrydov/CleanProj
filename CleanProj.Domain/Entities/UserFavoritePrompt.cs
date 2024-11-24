using CleanProj.Domain.Common;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

public class UserFavoritePrompt: BaseEntity
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public long PromptId { get; set; }
    public Prompt Prompt { get; set; }
    
}