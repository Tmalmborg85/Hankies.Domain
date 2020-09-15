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
        /// <summary>
        /// The customer who is responsible for this avatar.
        /// </summary>
        public ICustomer Owner { get; }

        /// <summary>
        /// A collection of session specific settings. Determines if cruise is
        /// active.  
        /// </summary>
        IEnumerable<IAvatarCruiseSession> Sessions { get; }

        /// <inheritdoc cref="IHandle.Handle"/>
        public IHandle Handle { get; }

        /// <summary>
        /// Indicates if an avatar can see other's photos by default. Immutable. 
        /// </summary>
        /// <remarks>
        /// Exceptions to the blindfold rule are granted in the avatar sessions
        /// </remarks>
        public bool Blindfolded { get; }

        /// <summary>
        /// Indicates if others can see this avatar's photos by default.
        /// Immutable.
        /// </summary>
        /// /// <remarks>
        /// Exceptions to the hooded rule are granted in the avatar sessions
        /// </remarks>
        public bool Hooded { get; }

        /// <summary>
        /// The first thing others see.   
        /// </summary>
        /// <remarks>
        /// A description can be used instead
        /// </remarks>
        public IPhoto ImpressionPhoto { get; }

        /// <summary>
        /// The first thing others read about you. 
        /// </summary>
        /// <remarks>
        /// Used in place of a photo. Not the same as being hooded.</remarks>
        public string ImpressionDescription { get; }

        /// <summary>
        /// An immutable collection of exposing photos. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        IEnumerable<IPhoto> ExposingPhotos { get; }

        /// <summary>
        /// An immutable collection of handkerchiefs. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        IEnumerable<IHandkerchief> Handkerchiefs { get; }

        /// <summary>
        /// When the current session expires. Can be extended.
        /// </summary>
        /// <returns>The experation date time for this session</returns>
        /// <remarks>
        /// Checks for any time extesnions and adds them to the original
        /// experation time.
        /// </remarks>
        public DateTimeOffset CurrentExperation();

        /// <summary>
        /// Avatars i can interact with in more detail.  
        /// </summary>
        /// <remarks>
        /// Avatars that I have cruised and have cruised me back and have an
        /// active session can be interacted with further.</remarks>
        /// <returns>A collection of interactable avatars.</returns>
        public IEnumerable<IAvatar> InteractableAvatars();

        /// <summary>
        /// Can I see another Avatar's impression photo?
        /// </summary>
        /// <remarks>
        /// A number of factors can influance if another avatar can be seen.
        /// You could be wearing a blindfold. They could be excpempt from that
        /// blindfold. They could be wearing a hood preventing you from seeing
        /// them.</remarks>
        /// <param name="them">The avatar I am atempting to see</param>
        /// <returns></returns>
        bool CanISeeThem(IAvatar them);

        /// <summary>
        /// Can another Avatar see me? 
        /// </summary>
        /// <param name="they">The avatar I want to check</param>
        /// <remarks>
        /// Lots of things can alter if another avatar can see you. namly if
        /// you are wearing a hood and they are not except from it. also they
        /// may be wearing a blindfold. 
        /// </remarks>   
        /// <returns></returns>
        bool CanTheySeeMe(IAvatar they);


        #region Actions

        /// <summary>
        /// Starts a new cruise session of this <c>Avatar</c>.
        /// </summary>
        /// <param name="coordinates">The starting location of this cruise.</param>
        /// <param name="time">The initial amount of tuime to cruise for</param>
        /// <returns></returns>
        IStatus<IAvatarCruiseSession> StartNewCruiseSession(ICruiseCoordinates
            coordinates, TimeSpan time);

        /// <summary>
        /// Stops the current cruise session. 
        /// </summary>
        /// <returns></returns>
        void EndCruiseSession();

        /// <summary>
        /// Extend the current session with a time extension object. 
        /// </summary>
        /// <param name="timeExtension">Typicaly a purchased extension</param>
        /// <returns>A status indicating success or not</returns>
        IStatus<IAvatarCruiseSession> ExtendCurrentSession(ITimeExtension timeExtension);

        /// <summary>
        /// Cruise an avatar.
        /// </summary>
        /// <remarks>
        /// Adds to my sessions cruised list and generates an event that
        /// triggers an avatars CruisedBy action.
        /// </remarks>
        /// <param name="cruisee"></param>
        void CruiseAnAvatar(IAvatar cruisee);

        /// <summary>
        /// Be cruised by an avatar. 
        /// </summary>
        /// <remarks>
        /// Adds a cruise to the cruised by collection in response to an
        /// event</remarks>
        /// <param name="cruisee"></param>
        void WasCruisedBy(IAvatar cruisee);

        /// <summary>
        /// Look for avatars that match enouch attributes to be cruiseable. 
        /// </summary>
        /// <returns></returns>
        IStatus<IAvatar> SearchForCruisableAvatars();

        /// <summary>
        /// Take blinfold off for a specific avatar. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        IStatus<IAvatar> DoffBlindfoldFor(IAvatar them);

        /// <summary>
        /// Put your blindfold back on for an avatar. if you are waering one. 
        /// </summary>
        /// <param name="them">Who you are re donning your blindfold for</param>
        /// <returns></returns>
        IStatus<IAvatar> ReDonBlindfoldFor(IAvatar them);

        /// <summary>
        /// Take your hood off for a specific avatar. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        IStatus<IAvatar> DoffHoodFor(IAvatar them);

        /// <summary>
        /// Put your hood back on if you are waering one. 
        /// </summary>
        /// <remarks>
        /// Does not persist over sessions, like consent.</remarks>
        /// <param name="them">Who you are doffing your blindfold for</param>
        /// <returns></returns>
        IStatus<IAvatar> ReDonHoodFor(IAvatar them);

        /// <summary>
        /// Sends a message to a customer.
        /// </summary>
        /// <remarks>
        /// Sends a message to a customer via there avatar. creates a new
        /// conversation if none exsists.
        /// </remarks>
        /// <param name="message"></param>
        IStatus<IAvatar> SendMessage(IChatMessage message);
        #endregion
    }
}
    