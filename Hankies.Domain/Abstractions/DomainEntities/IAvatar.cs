using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    
    public interface IAvatar : IDeletableDomainEntity, IEqualityComparer<IAvatar>
    {
        #region Properties
        
        /// <summary>
        /// Avatars i can interact with in more detail.  
        /// </summary>
        /// <remarks>
        /// Avatars that I have cruised and have cruised me back and have an
        /// active session can be interacted with further.</remarks>
        /// <returns>A collection of interactable avatars.</returns>
        public IEnumerable<IAvatar> InteractableAvatars();

        
        /// <summary>
        /// Is this cruise currently active
        /// </summary>
        public bool HasActiveCruiseSession { get; }

        #endregion
        #region Actions

        /// <summary>
        /// Starts a new cruise session of this <c>Avatar</c>.
        /// </summary>
        /// <param name="coordinates">The starting location of this cruise.</param>
        /// <param name="time">The initial amount of tuime to cruise for</param>
        /// <returns></returns>
        public IStatus<ICruise> StartNewCruiseSession(ICoordinates
            coordinates, TimeSpan time);

        /// <summary>
        /// Stops the current cruise session. 
        /// </summary>
        /// <returns></returns>
        public void EndCruiseSession();

        /// <summary>
        /// Extend the current session with a time extension object. 
        /// </summary>
        /// <param name="timeExtension">Typicaly a purchased extension</param>
        /// <returns>A status indicating success or not</returns>
        public IStatus<ICruise> ExtendCurrentSession(ITimeExtension timeExtension);

        /// <summary>
        /// Cruise an avatar.
        /// </summary>
        /// <remarks>
        /// Adds to my sessions cruised list and generates an event that
        /// triggers an avatars CruisedBy action.
        /// </remarks>
        /// <param name="cruisee"></param>
        public void CruiseAnAvatar(IAvatar cruisee);

        /// <summary>
        /// Be cruised by an avatar. 
        /// </summary>
        /// <remarks>
        /// Adds a cruise to the cruised by collection in response to an
        /// event</remarks>
        /// <param name="cruisee"></param>
        public void WasCruisedBy(IAvatar cruisee);

        /// <summary>
        /// Look for avatars that match enouch attributes to be cruiseable. 
        /// </summary>
        /// <returns></returns>
        public IStatus<IAvatar> SearchForCruisableAvatars();

        /// <summary>
        /// Take blinfold off for a specific avatar. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        public IStatus<IAvatar> DoffBlindfoldFor(IAvatar them);

        /// <summary>
        /// Put your blindfold back on for an avatar. if you are waering one. 
        /// </summary>
        /// <param name="them">Who you are re donning your blindfold for</param>
        /// <returns></returns>
        public IStatus<IAvatar> ReDonBlindfoldFor(IAvatar them);

        /// <summary>
        /// Take your hood off for a specific avatar. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        public IStatus<IAvatar> DoffHoodFor(IAvatar them);

        /// <summary>
        /// Put your hood back on if you are waering one. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        public IStatus<IAvatar> ReDonHoodFor(IAvatar them);

        /// <summary>
        /// Sends a message to a customer.
        /// </summary>
        /// <remarks>
        /// Sends a message to a customer via there avatar. creates a new
        /// conversation if none exsists.
        /// </remarks>
        /// <param name="message"></param>
        public IStatus<IAvatar> SendMessage(IChatMessage message);
        #endregion
    }
}
