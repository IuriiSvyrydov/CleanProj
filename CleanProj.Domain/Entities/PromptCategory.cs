
using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;

    public sealed class PromptCategory :BaseEntity
    {
        public long PromtId { get; set; }
        public Prompt Prompt { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
