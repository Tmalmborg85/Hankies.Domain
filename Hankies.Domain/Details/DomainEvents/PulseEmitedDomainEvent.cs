using System.Collections.Generic;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that lets the Application layer know a pulse has been emited. 
    /// </summary>
    public class PulseEmitedDomainEvent : DomainEvent
    {
        public PulseEmitedDomainEvent(double lattitude, double longitude,
            float radius, CruiseRadar source)
        {
            var pulseLocation = new EchoLocation(lattitude, longitude);
            Pulse = new RadarPulse(source, pulseLocation, radius);

            OnValidate();
        }

        public PulseEmitedDomainEvent(EchoLocation location, float radius,
            CruiseRadar source)
        {
            Pulse = new RadarPulse(source, location, radius);

            OnValidate();
        }

        public PulseEmitedDomainEvent(RadarPulse radarPulse)
        {
            Pulse = radarPulse;
            OnValidate();
        }

        /// <summary>
        /// Pulse emmited from a Radar object to look for echos. 
        /// </summary>
        public RadarPulse Pulse { get; private set; }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (!Pulse.IsValid)
            {
                // Add all Pulse Rule violations.
                foreach (var violation in Pulse.GetRuleViolations())
                {
                    yield return violation;
                }

                yield return new HankiesRuleViolation
                    ("Pulse Emmited events must have a valid pulse.", Pulse);
            }

            yield break;
        }

    }
}
