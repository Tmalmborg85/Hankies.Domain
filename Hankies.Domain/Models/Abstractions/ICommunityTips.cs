using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    public interface ICommunityTips
    {
        /// <summary>
        /// Community submited tips on flagging this way. 
        /// </summary>
        public IEnumerable<ICommunityTip> Tips { get; }

        /// <summary>
        /// Add an authored tip. 
        /// </summary>
        /// <param name="tip">The tip content to add</param>
        /// <param name="handle">How the tip will be attributed to others</param>
        /// <param name="author">Customer who owns this Tip</param>
        /// <param name="location">Where this tip is about</param>
        /// <returns></returns>
        IStatus<string> CreateNewTip(string tip, IExternalHandle handle,
            ICustomer author, ICommunityLocation location);

        /// <summary>
        /// Remove a tip that already is in the tip list. 
        /// </summary>
        /// <param name="tip">The tip, with ID, to remove</param>
        void RemoveTip(ICommunityTip tip);
    }
}
