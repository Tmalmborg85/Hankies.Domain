using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// An abstract object that represents what a handkerchief’s flagging means. 
    /// </summary>
    public interface IFlaggingAs : ICommunityTips
    {
        #region Properties

        /// <summary>
        /// What this flagging definition is primarily known as
        /// </summary>
        /// <example>
        /// Rigger, Top, Sub</example>
        public string KeyTitle { get; }

        /// <summary>
        /// Other titles used. 
        /// </summary>
        public IEnumerable<string> AlternateTitles { get; }

        /// <summary>
        /// A description of what flagging this way entails. 
        /// </summary>
        public string Description { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Add a distinct alternate title.
        /// </summary>
        /// <param name="title">the proposed title to add</param>
        /// <returns>A status with the new title</returns>
        IStatus<string> AddAlternateTitle(string title);

        /// <summary>
        /// remove an exsisting title if found
        /// </summary>
        /// <param name="title"></param>
        void RemoveAlternateTitle(string title);

        /// <summary>
        /// Try to change an alternate title
        /// </summary>
        /// <param name="oldTitle">The old title</param>
        /// <param name="newTitle">What to change it to</param>
        /// <returns>A status with the new title</returns>
        IStatus<string> UpdateAlternateTitle(string oldTitle, string newTitle);

        /// <summary>
        /// Change the current key title
        /// </summary>
        /// <param name="oldTitle">oldTitle / currentTitle</param>
        /// <param name="newTitle"></param>
        /// <returns></returns>
        /// <remarks>
        /// Add old keys to alt title if they are not already in the list.
        /// </remarks>
        IStatus<string> UpdateKeyTitle(string oldTitle, string newTitle);

        #endregion
    }
}
