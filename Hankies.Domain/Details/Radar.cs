using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details
{
    public class Radar : IRadar
    {

        public Radar(IDomainEntity ownedBy)
        {
            owner = ownedBy;
        }

        public HashSet<IRadarEcho<IAvatar>> _contacts { get; }
        public IEnumerable<IRadarEcho<IAvatar>> Contacts => _contacts?.ToList();

        public HashSet<IRadarEcho<IAvatar>> _clutter { get; }
        public IEnumerable<IRadarEcho<IAvatar>> Clutter => _clutter?.ToList();

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
            var radioEcho =

            if (_clutter.Contains(avatar))
        }

        public IStatus<IRadar> FlagAsClutter(IEnumerable<IAvatar> avatars)
        {
            throw new NotImplementedException();
        }
    }
}
