
using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;
    public sealed class PlaceHolder: BaseEntity
    {
        public string Name { get; set; }
        public Guid PropmptId { get; set; }
        public Prompt Prompt { get; set; }
        
        public static PlaceHolder Create(string name, Guid propmptId)
        => new PlaceHolder { Name = name, PropmptId = propmptId };
    }
