using System;
namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A Domain entity that can be marked as deleted. 
    /// </summary>
    public interface IDeletableDomainEntity
    {
        /// <summary>
        /// When the entity was deleted if it was.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; }

        /// <summary>
        /// If the entity is marked as deleted. 
        /// </summary>
        public bool Deleted { get; }

        /// <summary>
        /// Handles setting a entity to deleted.
        /// </summary>
        /// <param name="deletedTimestamp">When the entity was deleted</param>
        void DeletedEntity(DateTimeOffset deletedTimestamp);
    }
}
