using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
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
    public interface ICommunityTip : IVoteable<ICommunityTip>
    {
        /// <summary>
        /// Display name and Uri of who wrote the tip
        /// </summary>
        public IExternalHandle Handle { get; }

        /// <summary>
        /// The customer who owns this tip
        /// </summary>
        public ICustomer Author { get; }

        /// <summary>
        /// General area this tip was posted from. 
        /// </summary>
        public ICruiseCoordinates Location { get; }

        /// <summary>
        /// Complaintes about this tip. 
        /// </summary>
        public IEnumerable<ICommunityReport> Reports { get; }


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
        /// Update the handle's display text only
        /// </summary>
        /// <param name="newDisplayText"></param>
        /// <returns>a status indicating if update worked</returns>
        public IStatus<IExternalHandle> UpdateHandleDisplay(string newDisplayText);

        /// <summary>
        /// Update the handle's link and trusted platform
        /// </summary>
        /// <param name="newLink">The new link</param>
        /// <param name="newPlatform">The new social media platform to use</param>
        /// <returns>a status indicating if update worked</returns>
        public IStatus<IExternalHandle> UpdateHandleLink(Uri newLink,
            TrustedPlatforms newPlatform);

        /// <summary>
        /// Replace the handle with a new one
        /// </summary>
        /// <param name="newHandle">The new handle to replace with</param>
        /// <returns>a status indicating if update worked</returns>
        public IStatus<IExternalHandle> ReplaceHandle(IExternalHandle newHandle);

        /// <summary>
        /// Add a violation report from a distinct customer. 
        /// </summary>
        /// <param name="reporter">a distinct customer</param>
        /// <param name="reason">general reason this was reported</param>
        /// <param name="message">More detailed message</param>
        /// <returns></returns>
        public IStatus<ICommunityReport> Report(ICustomer reporter, ReportReasons reason,
            string message);

    }
}
