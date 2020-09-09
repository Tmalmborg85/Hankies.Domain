using System;
using System.Collections;
using System.Collections.Generic;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A persons expressive identity at a specific moment in time
    /// </summary>
    /// <example>
    /// Most other objects are owned by an IAvatar, reflecting their centricity
    /// to the Hankies domain. In the real world an IAvatar would be a person’s
    /// self identity at the moment when they cruise an area. This is done with
    /// handkerchiefs in their pockets which indicate attributes about themselfs
    /// and what they what they are looking for. Handkerchiefs can indicate
    /// anything from gender identity to kinks, sex rolls, and occupation.
    /// </example>
    public interface IAvatar
    {
        public ICustomer Owner { get; }
        public CruiseStatuses Status { get; }
        public IPhoto ImpressionPhoto { get; }

        public bool Blindfolded { get; }
        public bool Hooded { get; }
        public ICruiseCoordinates LastKnownLocation { get; }

        IEnumerable<IPhoto> ExposingPhotos { get; }
        IEnumerable<IHandkerchief> Handkerchiefs { get; }

        /// <summary>
        /// Other avatars this avatar has cruised.
        /// </summary>
        IEnumerable<IAvatar> CruisedAvatars { get; }

        /// <summary>
        /// Other avatars that have cruised this avatar
        /// </summary>
        IEnumerable<IAvatar> CruisedByAvatars { get; }

        /// <summary>
        /// Avatars that match the seeking gener, handkerchief rolls, and area
        /// </summary>
        IEnumerable<IAvatar> Matches { get; }

        /// <summary>
        /// Avatars that match two of three of these. Seeking gener,
        /// handkerchief rolls, and area
        /// </summary>
        IEnumerable<IAvatar> NearMatches { get; }


        void CruiseAvatar(IAvatar cruisee);
        void CruisedBy(IAvatar cruisee);

        IStatus<IAvatar> CruiseForAvatars();
        IStatus<IAvatar> PauseCruising();
        IStatus<IAvatar> EndCruising();

        IStatus<IAvatar> PutOnBlindfold();
        IStatus<IAvatar> TakeOffBlindfold();
        IStatus<IAvatar> PutOnHood();
        IStatus<IAvatar> TakeOffHood();

        public IEnumerable<IAvatar> WearHandkerchiefInPocket(IHandkerchief
            handkerchief, PocketTypes pocket);
        public IEnumerable<IAvatar> RemoveHandkerchiefFromPocket(IHandkerchief
            handkerchief, PocketTypes pocket);
    }
}
    