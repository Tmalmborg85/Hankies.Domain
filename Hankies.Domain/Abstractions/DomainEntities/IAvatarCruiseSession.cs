using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;

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
    public interface IAvatarCruiseSession : IDomainEntity
    {
        #region Properties
        /// <summary>
        /// When this session was started. Immutable.
        /// </summary>
        public DateTimeOffset StartedAt { get; }

        /// <summary>
        /// When this session was originaly set to expire.
        /// </summary>
        /// <remarks>
        /// Immutable. This is also used in calulation to determine the current
        /// experation time. </remarks>
        public DateTimeOffset OriginalExperation { get; }

        /// <summary>
        /// Collection of time extensions. can be empty. 
        /// </summary>
        IEnumerable<ITimeExtension> TimeExtensions { get; }

        /// <summary>
        /// Collection of locations cruise. 
        /// </summary>
        IEnumerable<ICruiseCoordinates> CruisedLocations { get; }

        /// <summary>
        /// Avatars that meet certain cruisable critira.
        /// </summary>
        IEnumerable<IAvatar> CruiseableAvatars { get; }

        /// <summary>
        /// Cruiseable avatars that I have cruise. 
        /// </summary>
        IEnumerable<IAvatar> CruisedAvatars { get; }

        /// <summary>
        /// Avatars that have cruised me or cruised me back. 
        /// </summary>
        IEnumerable<IAvatar> CruisedByAvatars { get; }

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
    }
}