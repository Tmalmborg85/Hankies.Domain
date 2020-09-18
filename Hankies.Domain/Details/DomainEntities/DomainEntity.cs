using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.DomainEntities
{
    /// <summary>
    /// Base Domain Entity. includes an ID and rule validation
    /// </summary>
    public abstract class DomainEntity : IDomainEntity
    {
        public DomainEntity()
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

        [Required]
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Modifiable list of domain events for an entity.  
        /// </summary>
        private List<IDomainEvent> _domainEvents;

        [NotMapped]
        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

        public IStatus<IDomainEntity> AddDomainEvent(IDomainEvent eventItem)
        {
            var addEventStatus = new Status<IDomainEntity>(true);

            if (_domainEvents == null)
                _domainEvents = new List<IDomainEvent>();

            if (eventItem == null)
                addEventStatus.AddError("Domain event can't be null.");

            if (_domainEvents.Contains(eventItem))
                addEventStatus.AddError("Can't contain duplicate events.");

                _domainEvents.Add(eventItem);

            addEventStatus.RespondWith(this);
            return addEventStatus;
        }

        public abstract IEnumerable<HankiesRuleViolation> GetRuleViolations();

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            if (_domainEvents == null)
                _domainEvents = new List<IDomainEvent>();

            if (_domainEvents.Contains(eventItem))
                _domainEvents.Remove(eventItem);
        }
    }
}
