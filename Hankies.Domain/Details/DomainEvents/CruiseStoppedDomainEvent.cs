using System;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents
{
    public class CruiseStoppedDomainEvent : CruiseStartedDomainEvent
    {
        public CruiseStoppedDomainEvent(Cruise cruise, Avatar avatar) :
            base(cruise, avatar)
        {
        }
    }
}
