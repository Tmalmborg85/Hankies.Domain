using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.DomainEntities;

namespace Hankies.Domain.Abstractions.ValueObjects
{
    public interface IRadarPulse : ICoordinates 
    {
        public DateTimeOffset EmittedAt { get; }
        public DateTimeOffset ReturnedAt { get; }
        public float Radius { get; }
        public bool HasEchos { get; }
        public IEnumerable<IAvatar> Echos { get; }
        public int EchoCount();
    }
}
