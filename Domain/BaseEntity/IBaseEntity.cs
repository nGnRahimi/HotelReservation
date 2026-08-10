using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.BaseEntity
{
    public interface IBaseEntity<K> where K : IEquatable<K>
    {
        K Id { get; }
        DateTime Created { get; set; }
        int? CreatedBy { get; set; }
        DateTime? Modified { get; set; }
        int? ModifiedBy { get; set; }
        bool Enable { get; set; }

        IReadOnlyCollection<INotification> DomainEvents { get; }
    }
}
