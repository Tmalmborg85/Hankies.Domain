using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Rules;

namespace Hankies.Domain.Details.DomainEntities
{
    /// <summary>
    /// A persons expressive identity at a specific moment in time
    /// </summary>
    /// <remarks>
    /// Most other objects are owned by an Avatar, reflecting their centricity
    /// to the Hankies domain. In the real world an IAvatar would be a person’s
    /// self identity at the moment when they cruise an area. This is done with
    /// handkerchiefs in their pockets which indicate attributes about themselfs
    /// and what they what they are looking for. Handkerchiefs can indicate
    /// anything from gender identity to kinks, sex rolls, and occupation.
    ///
    /// Avatars are the same if they contain the same handkerchief makeup,
    /// handle, photos, ect. Start time, locations, ect can be different.
    ///
    /// Avatars 
    /// </remarks>
    public class Avatar : DomainEntity, IDeletableDomainEntity,
        IEqualityComparer<Avatar>
    {
        const float DefaultRadarRange = 5.0f;

        #region Constructors

        /// <summary>
        /// For serialization and ORMs
        /// </summary>
        private Avatar() { }

        public Avatar(Guid id, DateTimeOffset createdAt, Customer owner,
            string handle, IEnumerable<Handkerchief> _handkerchiefs) : base
            (id, createdAt)
        {
            // Newly created avatars automatically start cruising.
            Customer = owner;
            Handle = handle;
            handkerchiefs = _handkerchiefs.ToHashSet();

            if (CruiseRadar == null)
                CruiseRadar = new CruiseRadar(this, DefaultRadarRange);

            if (cruises == null)
                cruises = new HashSet<Cruise>();

            if (photos == null)
                photos = new HashSet<IPhoto>();

            
            OnValidate();
        }
        #endregion

        #region Properties

        /// <summary>
        /// The customer who is responsible for this avatar.
        /// </summary>
        public Customer Customer { get; private set; }

        /// <summary>
        /// The last or current cruise. 
        /// </summary>
        public Cruise LastCruise { get; private set; }

        /// <summary>
        /// Contains a collection of nearby avatars that meet your criteria
        /// and tools to look for them
        /// </summary>
        CruiseRadar CruiseRadar { get; }

        /// <summary>
        /// The owninmg customers chat ID. Chat IDs belong to the
        /// customer object. 
        /// </summary>
        public Guid ChatId => Customer.ChatID;

        /// <summary>
        /// A human readable string indicating what others should call you.
        /// </summary>
        /// <example>
        /// Daddy, Pig, Gristle McThornbody
        /// </example>
        public string Handle { get; private set; }

        /// <summary>
        /// Indicates if an avatar can see other's photos by default. Immutable. 
        /// </summary>
        /// <remarks>
        /// Exceptions to the blindfold rule are granted in the avatar sessions
        /// </remarks>
        /// <example>False</example>
        public bool Blindfolded { get; private set; }

        /// <summary>
        /// Indicates if others can see this avatar's photos by default.
        /// Immutable.
        /// </summary>
        /// <remarks>
        /// Exceptions to the hooded rule are granted in the avatar sessions
        /// </remarks>
        /// <example>False</example>
        public bool Hooded { get; private set; }

        /// <summary>
        /// The first thing others see.   
        /// </summary>
        /// <remarks>
        /// A description can be used instead
        /// </remarks>
        public IPhoto ImpressionPhoto { get; private set; }

        /// <summary>
        /// The first thing others read about you. 
        /// </summary>
        /// <remarks>
        /// Used in place of a photo. Not the same as being hooded.
        /// </remarks>
        public string ImpressionDescription { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public bool Deleted { get; private set; }

        #endregion

        #region HashBackings

        /// <summary>
        /// Distinct cruise sessions this avatar has. 
        /// </summary>
        private HashSet<Cruise> cruises { get; }

        /// <summary>
        /// Hash set of all photos this customer owns
        /// </summary>
        private HashSet<IPhoto> photos { get; }

        /// <summary>
        /// Backing for both left and right displayed handkerchiefs
        /// </summary>
        private HashSet<Handkerchief> handkerchiefs { get; }

        #endregion

        #region Aggregates

        /// <summary>
        /// Distinct cruise sessions this avatar has, as a list. 
        /// </summary>
        public IList<Cruise> Cruises => cruises?.ToList();

        /// <summary>
        /// An immutable collection of exposing photos. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        public IList<IPhoto> ExposingPhotos => photos?
            .Where((IPhoto arg) => arg.Rating == PhotoRatings.NSFW)
            .ToList();

        /// <summary>
        /// An immutable collection of safe for work photos. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        public IList<IPhoto> SafeForWorkPhotos => photos?
            .Where((IPhoto arg) => arg.Rating == PhotoRatings.SFW)
            .ToList();

        /// <summary>
        /// An immutable collection of all handkerchiefs. 
        /// </summary>
        /// <remarks>
        /// Changing this would be a new avatar.
        /// </remarks>
        public IEnumerable<Handkerchief> Handkerchiefs => handkerchiefs.ToList();

        /// <summary>
        /// Only the handkerchiefs in the left pocket
        /// </summary>
        public IEnumerable<Handkerchief> LeftPocket => handkerchiefs
            .Where((Handkerchief hanky) => hanky.InPockets.Contains(PocketTypes.Left))
            .ToList();

        /// <summary>
        /// Only the handkerchiefs in the right pocket
        /// </summary>
        public IEnumerable<Handkerchief> RightPocket => handkerchiefs
            .Where((Handkerchief hanky) => hanky.InPockets.Contains(PocketTypes.Right))
            .ToList();

        #endregion

        #region Public Methods


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
        public bool ICanSeeThem(Avatar them)
        {
            var areTheyExemptFromMyBlindfold = LastCruise.IsBlindfoldRemovedForAvatar
                (them);

            var amIExemptFromThierHood = them.LastCruise.IsHoodRemovedForAvatar
                (this);

            if (Blindfolded && !areTheyExemptFromMyBlindfold)
                return false;

            if ((Blindfolded && areTheyExemptFromMyBlindfold) || !Blindfolded)
            {
                if (them.Hooded && !amIExemptFromThierHood)
                {
                    return false;
                }
            }

            // Does not meet any rules for not being seen. 
            return true;
        }

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
        public bool TheyCanSeeMe(Avatar they)
        {
            return they.ICanSeeThem(this);
        }

        /// <summary>
        /// Gets the experation of the last cruise. 
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset CurrentExperation()
        {
            var lastEXPTime = LastCruise.HasTimeRemaining();
        }
        #endregion

        #region Actions

        public void CruiseAvatar(Avatar cruisee)
        {
            //LastSession.CruisedAvatars
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods

        #endregion
        public bool HasActiveCruiseSession => throw new NotImplementedException();

        
        

        public bool HardPassedOnHandkerchied(Handkerchief handkerchief)
        {

        }

        public bool HardPassedOnAnyOfTheseHandkerchiefs
            (IEnumerable<Handkerchief> handkerchiefs)
        {

        }

        

        

        public void DeletedEntity(DateTimeOffset deletedTimestamp)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> DoffBlindfoldFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> DoffHoodFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public void EndCruiseSession()
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] IAvatar x, [AllowNull] IAvatar y)
        {
            throw new NotImplementedException();
        }

        public IStatus<ICruisee>> ExtendCurrentSession(ITimeExtension timeExtension)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] IAvatar obj)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (Customer == null)
            {
                yield return new HankiesRuleViolation
                    ("Avatars are owned by one customer.", Customer);
            }
            else if (Customer.Avatars != null && Customer.Avatars.Contains(this))
            {
                yield return new HankiesRuleViolation
                    ("Avatars are distinct per customer.", Customer.Avatars);
            }

            // Rule: Newly created avatars automatically start cruising.

            if (handkerchiefs == null || handkerchiefs.Count == 0)
                yield return new HankiesRuleViolation
                    ("Avatars must have at least one handkerchief", handkerchiefs);

            // Rule: Avatars must have a handle that is valid.
            var handleViolations = HandleRules.GetHandleRuleViolations(Handle);
            foreach (var violation in handleViolations)
            {
                yield return violation;
            }

            if (ImpressionPhoto == null && (string.IsNullOrEmpty
                (ImpressionDescription) || string.IsNullOrWhiteSpace
                (ImpressionDescription)))
                yield return new HankiesRuleViolation("Avatars must have an " +
                    "approved SFW photo, a pending aproval photo, or a " +
                    "description.", "Photo & Description");



            if (Sessions == null)
            {
                yield return new HankiesRuleViolation
                    ("One avatar owns many Sessions.", Sessions);
            }
            else if (LastSession == null)
            {
                yield return new HankiesRuleViolation
                    ("Every avatar has at least one session.", LastSession);
            }

            // Rule: Avatars are immutable exception for the Session aggregate.
            // Rule: “Editing” an avatar creates a clone with the changed properties.

            yield break;
        }

        public IEnumerable<IAvatar> InteractableAvatars()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> ReDonBlindfoldFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> ReDonHoodFor(IAvatar them)
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> SearchForCruisableAvatars()
        {
            throw new NotImplementedException();
        }

        public IStatus<IAvatar> SendMessage(IChatMessage message)
        {
            throw new NotImplementedException();
        }

        public IStatus<ICruisee>> StartNewCruiseSession(ICoordinates coordinates, TimeSpan time)
        {
            //var avatarSessionStartedDomainEvent = new AvatarSessionStartedDomainEvent();
            throw new NotImplementedException();
        }

        public void WasCruisedBy(IAvatar cruisee)
        {
            throw new NotImplementedException();
        }

        public IStatus<ICruise> StartNewCruiseSession(ICoordinates coordinates, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public IStatus<ICruise> ExtendCurrentSession(ITimeExtension timeExtension)
        {
            throw new NotImplementedException();
        }
    }
}
