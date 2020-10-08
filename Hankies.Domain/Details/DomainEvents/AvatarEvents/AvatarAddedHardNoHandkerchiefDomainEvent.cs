using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    internal class AvatarAddedHardNoHandkerchiefDomainEvent : AvatarDomainEvent
    {
        private Handkerchief handkerchief;

        public AvatarAddedHardNoHandkerchiefDomainEvent(Avatar avatar
            , Handkerchief handkerchief) : base (avatar)
        {
            this.handkerchief = handkerchief;
        }
    }
}