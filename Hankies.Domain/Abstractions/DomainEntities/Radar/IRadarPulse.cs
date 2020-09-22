using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;

namespace Hankies.Domain.Abstractions.DomainEntities.Radar
{
    /// <summary>
    /// A pulse sent out by an IRadar
    /// </summary>
    public interface IRadarPulse : ICoordinates 
    {
        /// <summary>
        /// The time this pulse was emitted from an IRadar
        /// </summary>
        public DateTimeOffset EmittedAt { get; }

        /// <summary>
        /// The IRadar that emitted this pulse
        /// </summary>
        public IRadar EmittedFrom { get; set; }

        /// <summary>
        /// How far this pulse should look from its lat/long
        /// </summary>
        public float Radius { get; }
    }
}
