using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Rules;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that lets the Application layer know a pulse has been emited. 
    /// </summary>
    public class PulseEmitedDomainEvent : DomainEvent, IRadarPulse
    {
        public PulseEmitedDomainEvent(double lattitude, double longitude,
            float radius, CruiseRadar source)
        {
            _emmitedAt = DateTimeOffset.UtcNow;
            _source = source;
            _radius = radius;
            _lattitude = lattitude;
            _longitude = longitude;
        }

        private DateTimeOffset _emmitedAt { get; }
        public DateTimeOffset EmittedAt => _emmitedAt;

        private CruiseRadar _source { get; }
        public IRadar Source => _source;

        private float _radius { get; }
        public float Radius => _radius;

        public double _lattitude { get; }
        public double Lattitude => _lattitude;

        public double _longitude { get; set; }
        public double Longitude => _longitude;

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (EmittedAt > DateTimeOffset.UtcNow)
                yield return new HankiesRuleViolation
                    ("A pulse cannot be emmited at a future time.", EmittedAt);

            if (_source == null)
                yield return new HankiesRuleViolation
                    ("Pulses must be emmited by a source", _source);

            if (_source.Owner.IsValid)
                yield return new HankiesRuleViolation
                    ("Pulses must be from a valid source", _source);

            if (_radius >= 500)
                yield return new HankiesRuleViolation
                    ("Pulse radius must be > 500 meters", _radius);

            if (_radius <= 40000)
                yield return new HankiesRuleViolation
                    ("Pulse radius must be < 40000 meters (24.8 Miles)", _radius);

            foreach (var violation in CoordinatesRules.
                GetCoordinatesRuleViolations(this))
            {
                yield return violation; 
            }

            yield break;
        }

    }
}
