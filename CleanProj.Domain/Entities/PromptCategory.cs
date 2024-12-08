
using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;

    public sealed class PromptCategory :BaseEntity
    {
        public Guid PromptId { get; set; }
        public Prompt Prompt { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public static PromptCategory Create(Guid promptId, Guid categoryId)
        => new PromptCategory { Id =Guid.CreateVersion7(), PromptId = promptId, CategoryId = categoryId };
        
    }
