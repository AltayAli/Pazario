namespace Pazario.Common.Domain.Abstractions
{
    public class BaseEntity : ICloneable
    {
        public Guid Id { get; init; }
        public Guid? AddedById { get; private set; }
        public DateTime? AddedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public Guid? ModifiedById { get; private set; }
        public DateTime? DeletedDate { get; private set; }
        public Guid? DeletedById { get; private set; }


        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _events;

        public void AddDomainEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void RemoveDomainEvent(IDomainEvent @event)
        {
            _events.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public void MarkAsAdded(Guid? addedBy, DateTime addedDate)
        {
            AddedById = addedBy;
            AddedDate = addedDate;
        }
        public void MarkAsModified(Guid? modifiedBy, DateTime modifiedDate)
        {
            AddedById = modifiedBy;
            AddedDate = modifiedDate;
        }
        public void MarkAsDeleted(Guid? deletedBy, DateTime deletedDate)
        {
            DeletedById = deletedBy;
            DeletedDate = deletedDate;
        }
    }
}
