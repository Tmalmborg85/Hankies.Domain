using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Rules;

namespace Hankies.Domain.Details.Radar
{
    /// <summary>
    /// A pulse sent out by an Cruise Radar. 
    /// </summary>
    public class RadarPulse : IValidateable
    {
        private RadarPulse() { }

        /// <summary>
        /// Construct a pulse from Souce, Location, and Radius
        /// </summary>
        /// <param name="source"></param>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        public RadarPulse(CruiseRadar source, EchoLocation location,
            float radius)
        {
            EmittedAt = DateTimeOffset.UtcNow;
            Source = source;
            Radius = radius;
            Location = location;
        }

        /// <summary>
        /// The time this pulse was emitted from an IRadar
        /// </summary>
        public DateTimeOffset EmittedAt { get; private set; }

        /// <summary>
        /// The IRadar that emitted this pulse
        /// </summary>
        public CruiseRadar Source { get; private set; }

        /// <summary>
        /// How far this pulse should look from its lat/long
        /// </summary>
        public float Radius { get; private set; }

        /// <summary>
        /// Location this pulse was emmited at
        /// </summary>
        public EchoLocation Location { get; }

        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }

        public IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (EmittedAt > DateTimeOffset.UtcNow)
                yield return new HankiesRuleViolation
                    ("A pulse cannot be emmited at a future time.",
                    EmittedAt);

            if (Source == null)
                yield return new HankiesRuleViolation
                    ("Pulses must be emmited by a source", Source);

            if (Source.Owner.IsValid)
                yield return new HankiesRuleViolation
                    ("Pulses must be from a valid source", Source);

            if (Radius >= 500)
                yield return new HankiesRuleViolation
                    ("Pulse radius must be > 500 meters", Radius);

            if (Radius <= 40000)
                yield return new HankiesRuleViolation
                    ("Pulse radius must be < 40000 meters (24.8 Miles)", Radius);

            foreach (var violation in CoordinatesRules.
                GetCoordinatesRuleViolations(Location))
            {
                yield return violation;
            }
        }

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
