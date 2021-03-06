﻿using System;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.Radar
{
    /// <summary>
	/// An object that is detectable by a <c>CruiseRadar</c>.
	/// </summary>
	/// <remarks>
    /// Add this interface to any implementation that can be detected by a
    /// <c>CruiseRadar</c>. Mostly implemented by <c>Avatars</c>. 
	/// </remarks>
    public interface IRadarDetectable : IEquatable<IRadarDetectable>
    {
		/// <summary>
        /// The unique ID that radars will use to track this item. 
        /// </summary>
        public Guid EchoID { get; }

        /// <summary>
        /// Position and time stamp detectable by radar.
        /// </summary>
        public EchoLocation Location { get; }

		/// <summary>
		/// Create a EchoDetectedDomainEvent in response to this objects echo
        /// detection rules/implementation. 
		/// </summary>
		/// <param name="pulse"></param>
		/// <returns></returns>
		public IStatus<EchoDetectedDomainEvent> Echo(RadarPulse pulse);
	}
}
