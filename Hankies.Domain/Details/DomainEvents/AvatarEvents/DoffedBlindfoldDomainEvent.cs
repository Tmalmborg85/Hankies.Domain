using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    public class DoffedBlindfoldDomainEvent : AvatarDomainEvent
    {
        public DoffedBlindfoldDomainEvent(Avatar avatar) : base(avatar)
        {
        }
    }
}
