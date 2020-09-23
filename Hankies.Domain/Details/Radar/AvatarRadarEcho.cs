using System;
using System.Diagnostics.CodeAnalysis;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;

namespace Hankies.Domain.Details.Radar
{
    public class AvatarRadarEcho : IRadarEcho<IAvatar>
    {
        public AvatarRadarEcho(IRadarPulse pulse, IRadarDetectable source,
            DateTimeOffset timestamp)
        {
            OriginatingPulse = pulse;
            EchoedAt = timestamp;
            EchoedFrom = source;
        }

        public IRadarPulse OriginatingPulse { get; private set; }

        public DateTimeOffset EchoedAt { get; private set; }

        public IRadarDetectable EchoedFrom { get; private set; }

        public bool Equals([AllowNull] IAvatar other)
        {
            if (other == null)
                return false;

            return EchoedFrom.EchoID == other.LastSession.EchoID;
        }
    }
}
