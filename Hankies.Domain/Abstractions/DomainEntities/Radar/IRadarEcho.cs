using System;
namespace Hankies.Domain.Abstractions.DomainEntities.Radar
{
    /// <summary>
    /// An immutable return radar echo. 
    /// </summary>
    /// <typeparam name="T">Type of object this radar echo is </typeparam>
    public interface IRadarEcho<T>
    {
        public IRadarPulse OriginatingPulse { get; }
        public DateTimeOffset EchoedAt { get; }
        public IRadarDetectable EchoedFrom { get; }
        
    }
}
