using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities.Radar
{
    /// <summary>
    /// A radar used to look for other IEchos (avatars) in an area. 
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
        /// Pulses this radar has emited. 
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
        /// Evatulate a single echo as Clutter or Contact.
        /// </summary>
        /// <param name="echo">The echo to evaluate</param>
        public void EvaluateEcho(IRadarEcho<IAvatar> echo);

        /// <summary>
        /// Evaluate many Echos as Clutter or Contacts.
        /// </summary>
        /// <param name="echos">The echos to evaluate.</param>
        public void EvaluateEchos(IEnumerable<IRadarEcho<IAvatar>> echos);

        /// <summary>
        /// Manualy flag an avatar as clutter for this radar, skipping the
        /// normal EvaluateEcho method.
        /// </summary>
        /// <param name="avatar">The avatar to be flagged</param>
        /// <returns>A status.</returns>
        /// <remarks>
        /// This could be used in case an Avatar later decides an IAvatar is
        /// clutter. If the IAvatar matches any avatars in Contacts, they
        /// should be removed. </remarks>
        IStatus<IRadar> FlagAsClutter(IAvatar avatar);

        /// <summary>
        /// Manualy flag multiple avatars as clutter for this radar, skipping
        /// the normal EvaluateEchos method.
        /// </summary>
        /// <param name="avatars">The avatars to be flagged</param>
        /// <returns>A status.</returns>
        /// <remarks>
        /// This would be a good place to pre-flag blocked customers.
        /// </remarks>
        IStatus<IRadar> FlagAsClutter(IEnumerable<IAvatar> avatars);
    }
}
