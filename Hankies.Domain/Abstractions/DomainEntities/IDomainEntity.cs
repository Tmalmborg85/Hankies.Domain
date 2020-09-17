using System;
using System.Collections.Generic;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A base domain entity
    /// </summary>
    public interface IDomainEntity
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
        /// Does this domain entity follow all its business rules?
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Checks for any business rule violations. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HankiesRuleViolation> GetRuleViolations();

        /// <summary>
        /// Throw a validation error if this entity is not valid. 
        /// </summary>
        public void OnValidate();
    }
}
