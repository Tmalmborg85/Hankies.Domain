using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.Radar
{
    public class CruiseRadar : IRadar
    {

        public CruiseRadar(IDomainEntity ownedBy)
        {
            owner = ownedBy;
        }

        public Dictionary<Guid, AvatarRadarEcho> _contacts { get; }
        public IEnumerable<IRadarEcho<IAvatar>> Contacts => _contacts?.Values
            .ToList();

        public Dictionary<Guid, AvatarRadarEcho> _clutter { get; }
        public IEnumerable<IRadarEcho<IAvatar>> Clutter => _clutter?.Values
            .ToList();

        public HashSet<IRadarPulse> _pulses { get; }
        public IEnumerable<IRadarPulse> Pulses => _pulses?.ToList();

        private IDomainEntity owner { get; }
        public IDomainEntity Owner => owner;

        private float _range { get; }
        public float Range => _range;

        public IStatus<PulseEmitedDomainEvent> EmitPulse(ICoordinates location, float radius)
        {
            var resultStatus = new Status<PulseEmitedDomainEvent>();
            var pulseEvent = new PulseEmitedDomainEvent(location.Lattitude,
                location.Longitude, Range, this);

            try
            {
                if (pulseEvent.IsValid)
                {
                    Owner.AddDomainEvent(pulseEvent);
                }
                else
                {
                    resultStatus.AddError("Invalid PulseEmitedDomainEvent, " +
                        "could not add to domain events");
                }
            }
            catch (Exception ex)
            {
                resultStatus.AddException(ex);
            }

            resultStatus.RespondWith(pulseEvent);
            return resultStatus;
        }

        public void EvaluateEcho(IRadarEcho<IAvatar> echo)
        {
            throw new NotImplementedException();
        }

        public void EvaluateEchos(IEnumerable<IRadarEcho<IAvatar>> echos)
        {
            throw new NotImplementedException();
        }

        public IStatus<IRadar> FlagAsClutter(IAvatar avatar)
        {
            // Make a Radr Echo from the provided avatar that can be added to
            // clutter
            var clutterKey = avatar.LastSession.EchoID;
            var clutterValue = new AvatarRadarEcho()



            if (!_clutter.ContainsKey(clutterKey))
            {
                _clutter.Add(clutterKey, avatar);
            }

        }

        public IStatus<IRadar> FlagAsClutter(IEnumerable<IAvatar> avatars)
        {
            throw new NotImplementedException();
        }
    }
}
