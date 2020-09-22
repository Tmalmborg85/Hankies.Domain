using System;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.DomainEvents;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Hankies.Domain.Abstractions;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that lets the Application layer know a pulse has been emited. 
    /// </summary>
    public class PulseEmitedDomainEvent : IRadarPulse, IDomainEvent, IValidateable
    {
        public PulseEmitedDomainEvent(double lattitude, double longitude,
            float radius, IRadar source)
        {
            _emmitedAt = DateTimeOffset.UtcNow;
            _emmitedFrom = source;
            _radius = radius;
            _lattitude = lattitude;
            _longitude = longitude;
        }

        private DateTimeOffset _emmitedAt { get; }
        public DateTimeOffset EmittedAt => _emmitedAt;

        private IRadar _emmitedFrom { get; }
        public IRadar EmittedFrom => _emmitedFrom;

        private float _radius { get; }
        public float Radius => _radius;

        public double _lattitude { get; }
        public double Lattitude => _lattitude;

        public double _longitude { get; set; }
        public double Longitude => _longitude;

        public bool IsValid => throw new NotImplementedException();

        public IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            throw new NotImplementedException();
        }

        public void OnValidate()
        {
            throw new NotImplementedException();
        }
    }
}
