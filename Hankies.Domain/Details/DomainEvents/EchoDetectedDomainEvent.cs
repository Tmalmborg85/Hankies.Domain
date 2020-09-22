using System;
using Hankies.Domain.Abstractions.DomainEntities.Radar;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Abstractions.ValueObjects;

namespace Hankies.Domain.Details.DomainEvents
{
    public class EchoDetectedDomainEvent : IDomainEvent
    {
        public EchoDetectedDomainEvent()
        {
        }
    }
}
