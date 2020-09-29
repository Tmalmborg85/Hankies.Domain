using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Details.ValueObject;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.Radar
{
    /// <summary>
    /// An immutable return radar echo. 
    /// </summary>
    public class RadarEcho : IEquatable<RadarEcho>, IValidateable
    {
        private RadarEcho() { }

        public RadarEcho(RadarPulse pulse, Cruise detectedCruise)
        {
            OriginatingPulse = pulse;
            EchoedAt = DateTimeOffset.UtcNow;
            Source = detectedCruise;
        }
        /// <summary>
        /// The pulse that triggerd this echo. 
        /// </summary>
        public RadarPulse OriginatingPulse { get; }

        /// <summary>
        /// When this echo was triggerd
        /// </summary>
        public DateTimeOffset EchoedAt { get; }

        public HandkerchiefMatchResult MatchResults { get; }

        /// <summary>
        /// The radar detectable object the Originating Pulse 'bounced' off of. 
        /// </summary>
        public Cruise Source { get; }

        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }
        /// <summary>
        /// Radr echos that are emmited at the same time and have the same
        /// Source are the same. 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] RadarEcho other)
        {
            return (Source.EchoID == other.Source.EchoID) &&
                (EchoedAt == other.EchoedAt);
        }

        public IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (!OriginatingPulse.IsValid)
            {
                // Add all Pulse Rule violations.
                foreach (var violation in OriginatingPulse.GetRuleViolations())
                {
                    yield return violation;
                }

                yield return new HankiesRuleViolation
                    ("Originating Pulse must be valid.", OriginatingPulse);
            }

            if (EchoedAt > DateTimeOffset.UtcNow)
                yield return new HankiesRuleViolation
                    ("Echos can only happen in the present or past.", EchoedAt);

            if (Source == null)
                yield return new HankiesRuleViolation
                    ("Echos must have an echo source", Source);

            yield break;
        }

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException
                    ("Hankies rule violations prevent saving this Domain Event");
        }
    }
}
