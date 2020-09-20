using System;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// Tips from customers tied to a location
    /// </summary>
    /// <remarks>
    /// Community tips are a great way for the community to contribute long term
    /// content in a safe way that will enhance everyones experience. Tips are
    /// location based reflecting the true nature of how local kink communitys
    /// work. The leather scene in new york is VERY different for Seattle for
    /// example. These differences tend to be washed out as user bases grow and
    /// homonegeization takes over. I belive for Hankies to truly thrive these
    /// differences need to be allowed to exist </remarks>
    public interface ICommunityTip : IVoteable<ICommunityTip>, IReportableContent
    {
        #region Properties

        /// <summary>
        /// Display name and Uri of who wrote the tip
        /// </summary>
        public string Attribution { get; }

        /// <summary>
        /// The customer who owns this tip
        /// </summary>
        public ICustomer Author { get; }

        /// <summary>
        /// General area this tip was posted from. 
        /// </summary>
        public ICCooordinates Location { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Removes the handle from this tip. 
        /// </summary>
        /// <remarks>
        /// A customer may rethink if they want to have a tip attributed to
        /// them and linked to a social meadia account. there needs to be a way
        /// to remove that attribution
        /// </remarks>
        public void RemoveAttribution();

        /// <summary>
        /// Update the handle's display text
        /// </summary>
        /// <param name="newDisplayText"></param>
        /// <returns>a status indicating if update worked</returns>
        public IStatus<ICommunityTip> UpdateHandleDisplay(string newDisplayText);
        #endregion
    }
}
