
using CleanProj.Domain.Entities;

namespace CleanProj.Domain.Common;

    public abstract class BaseEntity: ICreatedByEntity,IModifiedByEntity
    {
        public long Id { get; set; }
        public string?  CreatedByUserId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public string ? ModifiedByUserId { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }
