
namespace CleanProj.Domain.Common;

    public abstract class BaseEntity: ICreatedByEntity,IModifiedByEntity
    {
        public Guid Id { get; set; }
        public string?  CreatedByUserId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public string ? ModifiedByUserId { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        private readonly List<IDomainEvent> _domainEvents  = [];
        public IReadOnlyList<IDomainEvent>DomainEvents => _domainEvents.AsReadOnly();
        protected void AddDomainEvent(IDomainEvent domainEvent)=>_domainEvents.Add(domainEvent);
        protected void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Clear();
    }
