using System;
using Hankies.Domain.Abstractions.DomainEntities.Radar;
using Hankies.Domain.Abstractions.DomainEvents;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that lets the Application layer know a pulse has been emited. 
    /// </summary>
    public class PulseEmitedDomainEvent : IRadarPulse, IDomainEvent
    {
        public PulseEmitedDomainEvent(IRadarPulse pulse)
        {
        }
    }
}
