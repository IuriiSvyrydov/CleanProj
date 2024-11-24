
using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;
    public sealed class PlaceHolder: BaseEntity
    {
        public string Name { get; set; }
        public long PropmptId { get; set; }
        public Prompt Prompt { get; set; }
    }
