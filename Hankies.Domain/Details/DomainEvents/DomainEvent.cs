using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEvents
{
    public abstract class DomainEvent : IDomainEvent, IValidateable
    {
        public DomainEvent()
        {
        }

        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }

        public abstract IEnumerable<HankiesRuleViolation> GetRuleViolations();

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException
                    ("Hankies rule violations prevent saving this Domain Event");
        }
    }
}
