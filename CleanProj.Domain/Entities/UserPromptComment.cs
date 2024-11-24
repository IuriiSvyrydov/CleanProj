using CleanProj.Domain.Common;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

public sealed class UserPromptComment:BaseEntity
{
    public long? PromptComponentId { get; set; }
    public UserPromptComment ParentComment { get; set; }
    public string Content { get; set; }
    public long PromptId { get; set; }
    public Prompt Prompt { get; set; }
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<UserPromptComment> ChildComments { get; set; } = [];

}