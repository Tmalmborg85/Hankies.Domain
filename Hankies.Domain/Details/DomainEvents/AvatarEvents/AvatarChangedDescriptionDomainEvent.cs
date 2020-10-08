using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    public class AvatarChangedDescriptionDomainEvent : AvatarDomainEvent
    {
        public AvatarChangedDescriptionDomainEvent(Avatar avatar) :
            base(avatar)
        {
        }
    }
}
