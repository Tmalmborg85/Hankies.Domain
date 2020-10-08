using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    public class AvatarRemovedPhotoDomainEvent : AvatarDomainEvent
    {
        public AvatarRemovedPhotoDomainEvent(Avatar avatar) :
            base(avatar)
        {
            Avatar = avatar;
        }
    }
}
