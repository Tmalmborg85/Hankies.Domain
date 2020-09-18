using System;
using Hankies.Domain.Abstractions.DomainEvents;

namespace Hankies.Domain.Details.DomainEvents
{
    // When handled this event is going to add the new cruise session's parrent avatar to apropriate "cruisable avatars" of nearby avatars... AVATARS!
    public class AvatarCruiseSessionCreatedDomainEvent : IDomainEvent
    {
        public AvatarCruiseSessionCreatedDomainEvent()
        {
        }
    }
}
