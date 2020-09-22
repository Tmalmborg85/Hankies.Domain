using System;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.Radar
{
    /// <summary>
    /// Time stamped coordinates detectable by IRadars
    /// </summary>
    public interface IEchoLocation : ICoordinates
    {
        /// <summary>
        /// When the coordnates were recorded. 
        /// </summary>
        public DateTimeOffset TimeStamp { get; }

        /// <summary>
        /// Update the echo location with new coordinates. 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>Updates the EchoLocation's coordinates and sets a new timestamp on DateTimeOffset.Now()</returns>
		public IStatus<IEchoLocation> Update(ICoordinates coordinates);

        /// <summary>
        /// If an object has not moved just it timestamp can be updated 
        /// </summary>
        public IStatus<IEchoLocation> Refresh { get; }

        /// <summary>
        /// How stale is the location
        /// </summary>
        /// <returns></returns>
        public StalenessTypes Staleness();
    }
}
