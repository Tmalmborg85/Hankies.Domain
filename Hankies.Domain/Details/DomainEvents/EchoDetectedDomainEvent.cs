using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEvents
{
    /// <summary>
    /// An event that triggers when a detectable object recives a pulse.
    /// handles sending echo data back to the sender
    /// </summary>
    public class EchoDetectedDomainEvent : DomainEvent
    {
        public EchoDetectedDomainEvent(RadarEcho echo)
        {
            Echo = echo;
        }

        public EchoDetectedDomainEvent(RadarPulse pulse,
            IRadarDetectable detectedRadarObject)
        {
            var radarEcho = new RadarEcho(pulse, detectedRadarObject);
            Echo = radarEcho;
        }

        /// <summary>
        /// Echo created by an object that recived a Radar Pulse
        /// </summary>
        public RadarEcho Echo { get; }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (!Echo.IsValid)
            {
                foreach (var violation in Echo.GetRuleViolations())
                {
                    yield return violation;
                }

                yield return new HankiesRuleViolation
                    ("Echo Detected events require a valid echo", Echo);
            }
        }
    }
}