using System;
using System.Collections;
using System.Collections.Generic;

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
        IEnumerable<IAvatar> Cruised { get; }

        /// <summary>
        /// Other avatars that have cruised this avatar
        /// </summary>
        IEnumerable<IAvatar> CruisedBy { get; }

        IEnumerable<IAvatar> ClosestAvatars
    }
}
    