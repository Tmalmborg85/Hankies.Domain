using System;
using System.Collections.Generic;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Abstractions.DomainEvents
{
    public abstract class AvatarDomainEvent : DomainEvent
    {
        public AvatarDomainEvent(Avatar avatar)
        {
            Avatar = avatar;

            OnValidate();
        }

        public Avatar Avatar { get; set; }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (!Avatar.IsValid)
            {
                // Add all Avatar Rule violations.
                foreach (var violation in Avatar.GetRuleViolations())
                {
                    yield return violation;
                }

                yield return new HankiesRuleViolation
                    ("Avatar Changed Impression Photo DomainEvents must have" +
                    " a valid Avatar.", Avatar);
            }
            yield break;
        }
    }
}
