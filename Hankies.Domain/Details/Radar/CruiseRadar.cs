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

        public Dictionary<Guid, IRadarDetectable> _contacts { get; }
        public IEnumerable<IRadarDetectable> Contacts => _contacts?.Values
            .ToList();

        public Dictionary<Guid, IRadarDetectable> _clutter { get; }
        public IEnumerable<IRadarDetectable> Clutter => _clutter?.Values
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

        public IStatus<IRadar> FlagAsClutter(IRadarDetectable detectedObject)
        {
            var response = new Status<IRadar>();

            var clutterKey = detectedObject.EchoID;
            var clutterValue = detectedObject;

            if (detectedObject == null)
                response.AddError("Object to flag as clutter cannot be null");

            if (clutterKey == null)
                response.AddError("Clutter must have a GUID key to be added " +
                    "to clutter the collection");

            if (!_clutter.ContainsKey(clutterKey))
            {
                _clutter.Add(clutterKey, clutterValue);

                RemoveClutterFromContact(clutterKey);
            }

            response.RespondWith(this);
            return response;
        }

        public IStatus<IRadar> FlagAsClutter(IEnumerable<IRadarDetectable> detectedObjects)
        {
            var response = new Status<IRadar>();

            if (detectedObjects == null)

        }

        private void RemoveClutterFromContact(Guid clutterKey)
        {
            if (_contacts.ContainsKey(clutterKey))
                _contacts.Remove(clutterKey);
        }
    }
}
