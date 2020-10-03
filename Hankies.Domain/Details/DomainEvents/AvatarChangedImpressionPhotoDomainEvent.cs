using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents
{
    public class AvatarChangedImpressionPhotoDomainEvent : AvatarDomainEvent
    {
        public AvatarChangedImpressionPhotoDomainEvent(Avatar avatar) :
            base(avatar)
        {
            Avatar = avatar;
        }
    }
}
