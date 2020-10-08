using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    public class DoffedHoodDomainEvent : AvatarDomainEvent
    {
        public DoffedHoodDomainEvent(Avatar avatar, Avatar doffedHoodFor) : base(avatar)
        {
        }
    }
}
