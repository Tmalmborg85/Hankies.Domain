using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEntities
{
    /// <summary>
    /// Base Domain Entity. includes an ID and rule validation
    /// </summary>
    public abstract class DomainEntity : IDomainEntity
    {
        private DomainEntity()
        {
        }

        public DomainEntity(Guid id, DateTimeOffset createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        [NotMapped]
        /// <inheritdoc cref="IDomainEntity.IsValid"/>
        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }

        [Key]
        public Guid Id { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public abstract IEnumerable<HankiesRuleViolation> GetRuleViolations();

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
