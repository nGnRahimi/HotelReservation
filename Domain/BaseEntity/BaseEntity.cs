using Domain.BaseEntity;
using MediatR;
using System.Text.Json.Serialization;

public abstract class BaseEntity<K> : IBaseEntity<K> where K : IEquatable<K>
{
    public K Id { get; protected set; }

    public DateTime Created { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public int? ModifiedBy { get; set; }

    public bool Enable { get; set; } = true;

    private List<INotification>? _domainEvents;

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    [JsonIgnore]
    public IReadOnlyCollection<INotification> DomainEvents =>
        _domainEvents?.AsReadOnly();
}