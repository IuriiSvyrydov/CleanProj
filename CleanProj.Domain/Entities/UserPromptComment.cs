using CleanProj.Domain.Common;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

public sealed class UserPromptComment : BaseEntity
{
    public int Level { get; set; }
    public string Content { get; set; }
    public  Guid PromptId { get; set; }
    public Prompt Prompt { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid? ParentCommentId { get; set; }
    public UserPromptComment  ParentComment { get; set; }
   
    public ICollection<UserPromptComment> ChildComments { get; set; } = [];
}