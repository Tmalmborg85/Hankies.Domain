using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A base domain entity
    /// </summary>
    public interface IDomainEntity : IValidateable
    {
        /// <summary>
        /// The unique ID of this entity.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// When this entity was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// A collection of side effects this domain entity should cause via
        /// events. 
        /// </summary>
        public IEnumerable<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Add a distinct event, aka side effect, to the events collection.
        /// </summary>
        /// <param name="eventItem"></param>
        public IStatus<IDomainEntity> AddDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Remove an event from the events list. 
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(IDomainEvent eventItem);
    }
}
