using System;
using System.Collections.Generic;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that lets the application layer know a cruise has been started
    /// by an Avatar
    /// </summary>
    public class CruiseStartedDomainEvent : DomainEvent
    {
        public CruiseStartedDomainEvent(Cruise cruise, Avatar avatar)
        {
            Avatar = avatar;
            Cruise = cruise;

            OnValidate();
        }

        /// <summary>
        /// The avatar that is starting the new cruise
        /// </summary>
        public Avatar Avatar { get; private set; }

        /// <summary>
        /// The cruise that is being started. 
        /// </summary>
        public Cruise Cruise { get; private set; }

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
                    ("Cruise Started events must have a valid Avatar.", Avatar);
            }

            if (!Cruise.IsValid)
            {
                // Add all Cruise Rule violations.
                foreach (var violation in Cruise.GetRuleViolations())
                {
                    yield return violation;
                }

                yield return new HankiesRuleViolation
                    ("Cruise Started events must have a valid Avatar.", Avatar);
            }
        }
    }
}
