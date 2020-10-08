using Hankies.Domain.Abstractions.DomainEvents;
using Hankies.Domain.Details.DomainEntities;

namespace Hankies.Domain.Details.DomainEvents.AvatarEvents
{
    internal class AvatarRemovedHardNoHandkerchiefDomainEvent :
        AvatarDomainEvent
    {
        private Handkerchief handkerchief;

        public AvatarRemovedHardNoHandkerchiefDomainEvent
            (Avatar avatar, Handkerchief handkerchief) : base (avatar)
        {
            this.handkerchief = handkerchief;
        }
    }
}