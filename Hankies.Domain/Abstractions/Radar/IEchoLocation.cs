using System;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.Radar
{
    /// <summary>
    /// Time stamped location detectable by <c>Cruise Radars</c>
    /// </summary>
    public interface IEchoLocation<T>
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
		public IStatus<T> Update(ICoordinates coordinates);

        /// <summary>
        /// If an object has not moved just it timestamp can be updated 
        /// </summary>
        public IStatus<T> Refresh();

        /// <summary>
        /// How stale is the location
        /// </summary>
        /// <returns></returns>
        public StalenessTypes Staleness();
    }
}
