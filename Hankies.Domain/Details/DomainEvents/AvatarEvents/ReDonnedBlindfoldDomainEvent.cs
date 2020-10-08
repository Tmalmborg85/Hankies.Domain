using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    public class ReDonnedBlindfoldDomainEvent : AvatarDomainEvent
    {
        public ReDonnedBlindfoldDomainEvent(Avatar avatar) : base(avatar)
        {
        }
    }
}
