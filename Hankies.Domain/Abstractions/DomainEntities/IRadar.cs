using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A radar used to look for other avatars while cruising. 
    /// </summary>
    public interface IRadar
    {
        /// <summary>
        /// IAvatars the radar has picked up. 
        /// </summary>
        /// <remarks>
        /// In the context of maritime radar the term Contact means any echo
        /// detected on the radarscope not evaluated as clutter or as a false
        /// echo.</remarks>
        public IEnumerable<IAvatar> Contacts { get; }

        /// <summary>
        /// IAvatars that dont match enouch factors to be considerd Contacts. 
        /// </summary>
        /// <remarks>
        /// In the context of actual radar "Clutter" refers to radio frequency
        /// (RF) echoes returned from targets which are uninteresting to the
        /// radar operators.
        /// </remarks>
        public IEnumerable<IAvatar> Clutter { get; }

        /// <summary>
        /// Pulses this radar has emited and gotten back. 
        /// </summary>
        IEnumerable<IRadarPulse> Pulses { get; }

        /// <summary>
        /// Emits a radar pulse via a PulseEmittedDomainEvent
        /// </summary>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        /// <remarks>
        /// Emitting a pulse triggers a PulseEmittedDomainEvent. Higher layers
        /// in Hankies handle the PulseEmittedDomainEvent and return a
        /// IRadarPulse to the radar object before saving to the database
        /// context.
        /// </remarks>
        IStatus<IRadar> EmitPulse(ICoordinates location, float radius);

        /// <summary>
        /// Return a pulse to this radar. 
        /// </summary>
        /// <param name="pulse"></param>
        public void PulseReturned(IRadarPulse pulse);

        /// <summary>
        /// Flag an avatar as clutter for this radar. 
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns></returns>
        IStatus<IRadar> FlagAsClutter(IAvatar avatar);

        /// <summary>
        /// Flag mutliple avatars as clutter. 
        /// </summary>
        /// <param name="avatars"></param>
        /// <returns></returns>
        IStatus<IRadar> FlagAsClutter(IEnumerable<IAvatar> avatars);
    }
}
