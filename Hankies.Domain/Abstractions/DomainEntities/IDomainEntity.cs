using System;

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
    }
}
