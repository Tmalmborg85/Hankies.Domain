﻿using System;
using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents
{
    public class AvatarAddedPhotoDomainEvent : AvatarDomainEvent
    {
        public AvatarAddedPhotoDomainEvent(Avatar avatar) :
            base(avatar)
        {
            Avatar = avatar;
        }
    }
}
