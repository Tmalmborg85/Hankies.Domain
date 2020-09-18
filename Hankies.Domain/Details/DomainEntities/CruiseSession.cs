using System;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Details.DomainEvents;

namespace Hankies.Domain.Details.DomainEntities
{
    public class AvatarCruiseSession : DomainEntity, IAvatarCruiseSession
    {
        public AvatarCruiseSession()
        {
            this.AddDomainEvent(new AvatarCruiseSessionCreatedDomainEvent());
        }
    }
}
