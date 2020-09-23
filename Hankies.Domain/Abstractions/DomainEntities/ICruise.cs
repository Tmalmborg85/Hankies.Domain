using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.DomainEntities.Radar;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// An avatars representation at a range in time. 
    /// </summary>
    /// <remarks>
    /// Avatar sessions are part of the overall avatar domain objcet. As such
    /// this can be an enimic model with no or minimalactions. Most if not all
    /// actions should be done by the owning avatar. 
    /// </remarks>
    public interface ICruise : IRadarDetectable
    {
        #region Properties
        /// <summary>
        /// When this session was started. Immutable.
        /// </summary>
        public DateTimeOffset StartedAt { get; }

        /// <summary>
        /// Contains a collection of nearby avatars that meet your criteria
        /// and tools to look for them
        /// </summary>
        IRadar Radar { get; }

        /// <summary>
        /// Avatars that I have cruised. 
        /// </summary>
        IEnumerable<IAvatar> Cruised { get; }

        /// <summary>
        /// Specific people the blind fold is off for. 
        /// </summary>
        /// <remarks>
        /// This only applies to avatars that are wearing a blindfold. 
        /// </remarks>
        IEnumerable<IAvatar> BlindfoldRemovedFor { get; }

        /// <summary>
        /// Specific people the hood is off for. 
        /// </summary>
        /// <remarks>
        /// This only applies to avatars that are wearing a hood. 
        /// </remarks>
        IEnumerable<IAvatar> HoodRemovedFor { get; }

        #endregion

        
        IStatus<ICruise> CruiseALocation(ICoordinates location);

        /// <summary>
        /// You wont see this avatar again this cruise. May see again in later
        /// cruise. VERY different from a block. 
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns>An IRadar. Check the Clutter collection for the added
        /// IAvatar</returns>
        /// <remarks>
        /// Avatar is marked as clutter in this cruises radar. 
        /// </remarks>
        IStatus<IRadar> PassAvatarThisTime(IAvatar avatar);
    }
}