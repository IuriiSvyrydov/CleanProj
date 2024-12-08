using CleanProj.Domain.Common;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

public class UserFavoritePrompt: BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid PromptId { get; set; }
    public Prompt Prompt { get; set; }
    
}