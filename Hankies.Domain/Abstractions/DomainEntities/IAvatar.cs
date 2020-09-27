using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A persons expressive identity at a specific moment in time
    /// </summary>
    /// <remarks>
    /// Most other objects are owned by an IAvatar, reflecting their centricity
    /// to the Hankies domain. In the real world an IAvatar would be a person’s
    /// self identity at the moment when they cruise an area. This is done with
    /// handkerchiefs in their pockets which indicate attributes about themselfs
    /// and what they what they are looking for. Handkerchiefs can indicate
    /// anything from gender identity to kinks, sex rolls, and occupation.
    ///
    /// Avatars are the same if they contain the same handkerchief makeup,
    /// handle, photos, ect. Start time, locations, ect can be different.  
    /// </remarks>
    public interface IAvatar : IDeletableDomainEntity, IEqualityComparer<IAvatar>
    {
        #region Properties

        /// <summary>
        /// The customer who is responsible for this avatar.
        /// </summary>
        public Customer CreatedByCustomer { get; }

        /// <summary>
        /// Gets an owner's chat ID
        /// </summary>
        public Guid ChatId => CreatedByCustomer.ChatID;

        /// <summary>
        /// A collection of cruise sessions this Avatar has started.
        /// </summary>
        public IEnumerable<ICruise> Sessions { get; }

        /// <summary>
        /// The last or current cruise session. 
        /// </summary>
        public ICruise LastSession { get; }

        /// <summary>
        /// A human readable string indicating what others should call you.
        /// </summary>
        /// <example>
        /// Daddy, Pig, Gristle McThornbody</example>
        public string Handle { get; }

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
        public IEnumerable<IPhoto> ExposingPhotos { get; }

        /// <summary>
        /// An immutable collection of safe for work photos. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        public IEnumerable<IPhoto> SafeForWorkPhotos { get; }

        /// <summary>
        /// An immutable collection of all handkerchiefs. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        public IEnumerable<Handkerchief> Handkerchiefs { get; }

        /// <summary>
        /// Only the handkerchiefs in the left pocket
        /// </summary>
        public IEnumerable<Handkerchief> LeftPocket { get; }

        /// <summary>
        /// Only the handkerchiefs in the right pocket
        /// </summary>
        public IEnumerable<Handkerchief> RightPocket { get; }

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
        public bool CanISeeThem(IAvatar them);

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
        public bool CanTheySeeMe(IAvatar they);

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
