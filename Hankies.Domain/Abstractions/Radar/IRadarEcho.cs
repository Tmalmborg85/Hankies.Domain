using System;
namespace Hankies.Domain.Abstractions.Radar
{
    /// <summary>
    /// An immutable return radar echo. 
    /// </summary>
    /// <typeparam name="T">Type of object this radar echo is </typeparam>
    public interface IRadarEcho<T> : IEquatable<T>
    {
        /// <summary>
        /// The pulse that triggerd this echo. 
        /// </summary>
        public IRadarPulse OriginatingPulse { get; }

        /// <summary>
        /// When this echo was triggerd
        /// </summary>
        public DateTimeOffset EchoedAt { get; }

        /// <summary>
        /// The radar detectable object the Originating Pulse 'bounced' off of. 
        /// </summary>
        public IRadarDetectable EchoedFrom { get; }
    }
}
