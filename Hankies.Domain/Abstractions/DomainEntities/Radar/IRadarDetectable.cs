using System;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities.Radar
{
    /// <summary>
	/// An object that is detectable by a Hankies IRadar.
	/// </summary>
	/// <remarks>
	/// </remarks>
    public interface IRadarDetectable
    {
		public IEchoLocation Location { get; }

		/// <summary>
		/// Create a EchoDetectedDomainEvent in response to this objects echo
        /// detection rules/implementation. 
		/// </summary>
		/// <param name="pulse"></param>
		/// <returns></returns>
		public IStatus<EchoDetectedDomainEvent> Echo(IRadarPulse pulse);
	}
}
