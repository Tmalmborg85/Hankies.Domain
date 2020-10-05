using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.Exceptions;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;
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
    /// </remarks>
    public class Avatar : DomainEntity, IDeletableDomainEntity,
        IEqualityComparer<Avatar>, IRadarDetectable
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

        /// <summary>
        /// Create a new Avatar with a random self-generated ID. 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="handle"></param>
        /// <param name="_handkerchiefs"></param>
        public Avatar (Customer owner, string handle,
            IEnumerable<Handkerchief> _handkerchiefs) : this (new Guid(),
                DateTimeOffset.UtcNow, owner, handle, _handkerchiefs){ }
        #endregion

        #region Properties

        /// <summary>
        /// The customer who is responsible for this avatar.
        /// </summary>
        public Customer Customer { get; private set; }

        /// <summary>
        /// If LastCruise is Null or not
        /// </summary>
        public bool HasLastCruise => LastCruise != null;

        /// <summary>
        /// The last cruise this Avatar created. 
        /// </summary>
        public Cruise LastCruise => Cruises.OrderBy(C => C.StartedAt)
            .FirstOrDefault();

        /// <summary>
        /// Checks to see if the Last cruise is still active
        /// </summary>
        public bool HasActiveCruise => ActiveCruise != null;

        /// <summary>
        /// The current active cruise. Can be null
        /// </summary>
        public Cruise ActiveCruise => GetActiveCruise();

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

        /// <summary>
        /// Handkerchiefs this avatar does not want to interact with.
        /// </summary>
        private HashSet<Handkerchief> hardNoHandkerchiefs { get; }
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
        /// Handkerchiefs this avatar never wants to see.
        /// </summary>
        /// <remarks>
        /// Avatars that are flagging with any of these handkerchiefs are
        /// flagged as clutter by the <c>CruiseRadar</c>
        /// </remarks>
        public IEnumerable<Handkerchief> HardNoHandkerchiefs =>
            hardNoHandkerchiefs.ToList();

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
        /// Checks if this Avatar has said no to this handkerchief. 
        /// </summary>
        /// <param name="handkerchief"></param>
        /// <returns></returns>
        public bool DidHardNoHandkerchief(Handkerchief handkerchief)
        {
            return hardNoHandkerchiefs.Contains(handkerchief);
        }

        /// <summary>
        /// Checks if an avatar has said no to any of these hankies. 
        /// </summary>
        /// <param name="handkerchiefs"></param>
        /// <returns></returns>
        public bool DidHardNoAnyOfTheseHandkerchiefs
            (IEnumerable<Handkerchief> handkerchiefs)
        {
            foreach (var hanky in handkerchiefs)
            {
                if (DidHardNoHandkerchief(hanky))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Actions

        /* Note on validation.
         * Not all actions require it. Only actions that are creating or
         * altering a setting from an unvalidated onbject, like a sring.
         */ 

        /// <summary>
        /// Creates a new Cruise for this avatar and triggers a domain event to
        /// let other objects know.
        /// </summary>
        /// <returns></returns>
        public IStatus<Avatar> StartNewCruise(Cruise cruiseToStart)
        {
            var response = new Status<Avatar>();

            try
            {
                AddDomainEvent(new CruiseStartedDomainEvent(cruiseToStart
                    , this));

                cruises.Add(cruiseToStart);

                response.RespondWithObject(this);
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        /// <summary>
        /// Stop the currenly active cruise, if there is one. 
        /// </summary>
        /// <returns></returns>
        public IStatus<Avatar> StopActiveCruise()
        {
            var response = new Status<Avatar>();

            try
            {
                if (HasActiveCruise)
                {
                    LastCruise.StoppedAt = DateTimeOffset.UtcNow;

                    AddDomainEvent(new CruiseStoppedDomainEvent
                        (LastCruise, this));

                    response.RespondWithObject(this);
                }
                else
                {
                    response.AddError("No active cruise avalable to stop.");
                }
                
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        /// <summary>
        /// Recive a radar pulse and return an echo. 
        /// </summary>
        /// <param name="pulse"></param>
        /// <returns></returns>
        public IStatus<Avatar> Echo(RadarPulse pulse)
        {
            var response = new Status<Avatar>();

            try
            {
                AddDomainEvent(new EchoDetectedDomainEvent(pulse, this));
                response.RespondWithObject(this);
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            return response;
        }

        /// <summary>
        /// Set or change the impression photo. 
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public IStatus<Avatar> SetImpressionPhoto(ImpressionPhoto photo)
        {
            var response = new Status<Avatar>();

            try
            {
                ImpressionPhoto = photo;

                AddDomainEvent(new AvatarChangedImpressionPhotoDomainEvent
                    (this));

                response.RespondWithObject(this);
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        /// <summary>
        /// Removes the impression photo. 
        /// </summary>
        /// <returns></returns>
        public void RemoveImpressionPhoto()
        {
            if (ImpressionPhoto != null)
            {
                ImpressionPhoto = null;
                AddDomainEvent(new AvatarChangedImpressionPhotoDomainEvent
                        (this));
            }
        }

        /// <summary>
        /// Add a rated photo to this Avatar. 
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public IStatus<Avatar> AddPhoto(RatedPhoto photo)
        {
            var response = new Status<Avatar>();

            try
            {
                photos.Add(photo);
                AddDomainEvent(new AvatarAddedPhotoDomainEvent
                    (this));

                response.RespondWithObject(this);
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        /// <summary>
        /// Set the impression description for this Avatar. 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IStatus<Avatar> SetImpressionDescription(string description)
        {
            var response = new Status<Avatar>();
            var oldDescription = ImpressionDescription;
            ImpressionDescription = description;

            if (!IsValid)
            {
                ImpressionDescription = oldDescription;
                foreach (var violation in GetRuleViolations())
                {
                    response.AddError(violation.Rule);
                }
            } 

            response.RespondWithObject(this);
            return response;
        }

        /// <summary>
        /// Adds a handkerchief to the Hard No list which will prevent this
        /// Avatar from seeing Avarats flagging with the specified handkerchief
        /// </summary>
        /// <param name="handkerchief">The valid handkerchief to be flaged</param>
        /// <returns></returns>
        public IStatus<Avatar> FlagHandkerchiefAsHardNo
            (Handkerchief handkerchief)
        {
            var response = new Status<Avatar>();

            try
            {
                hardNoHandkerchiefs.Add(handkerchief);
                AddDomainEvent(new AvatarAddedHardNoHandkerchiefDomainEvent
                    (this));

                response.RespondWithObject(this);
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        /// <summary>
        /// Removes a handkerchief from the hard no list if it was in it. 
        /// </summary>
        /// <param name="handkerchief"></param>
        /// <returns></returns>
        public IStatus<Avatar> StopFlaggingHandkerchiefAsHardNo
            (Handkerchief handkerchief)
        {
            var response = new Status<Avatar>();

            try
            {
                if (hardNoHandkerchiefs.Contains(handkerchief))
                {
                    hardNoHandkerchiefs.Remove(handkerchief);
                    AddDomainEvent(new AvatarRemovedHardNoHandkerchiefDomainEvent
                        (this));
                }
                else
                {
                    response.AddError
                        ("specified handkerchief not found in hard no list.");
                }
                
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            response.RespondWithObject(this);
            return response;
        }

        /// <summary>
        /// Removes your blindfold for a person, provided you are waering one. 
        /// </summary>
        /// <param name="them"></param>
        /// <returns></returns>
        public IStatus<Avatar> DoffBlindfoldFor(Avatar them)
        {
            var response = new Status<Avatar>();
            response = IsBlindfolded(response);

            if(response.IsSuccess())
            {
                response = LastCruise.DoffBlindfold(response, this, them);
            }

            response.RespondWithObject(this);
            return response;
        }

        /// <summary>
        /// Removed your hood for a person, provided you are waering one. 
        /// </summary>
        /// <param name="them"></param>
        /// <returns></returns>
        public IStatus<Avatar> DoffHoodFor(Avatar them)
        {
            var response = new Status<Avatar>();
            response = IsHooded(response);

            if (response.IsSuccess())
            {
                response = LastCruise.DoffHood(response, this, them);
            }
            response.RespondWithObject(this);
            return response;
        }

        /// <summary>
        /// Recinds an expection granted to your blindfold. 
        /// </summary>
        /// <param name="them"></param>
        /// <returns></returns>
        public IStatus<Avatar> ReDonBlindfoldFor(Avatar them)
        {
            var response = new Status<Avatar>();
            response = IsBlindfolded(response);
            
            if (response.IsSuccess())
                response = LastCruise.DonBlindfold(response, this, them);

            if (response.IsSuccess())

                response.RespondWithObject(this);
            return response;
        }

        /// <summary>
        /// Recinds an excpetion granted to your hood. 
        /// </summary>
        /// <param name="them"></param>
        /// <returns></returns>
        public IStatus<Avatar> ReDonHoodFor(Avatar them)
        {
            var response = new Status<Avatar>();
            response = IsHooded(response);

            if (response.IsSuccess())
            {
                response = LastCruise.DonHood(response, this, them);
            }
            response.RespondWithObject(this);
            return response;
        }


        public IStatus<Avatar> ExtendCruiseTime()
        {
            throw new NotImplementedException();
        }

        public IStatus<Avatar> SendMessage(IChatMessage message)
        {
            throw new NotImplementedException();
        }

        public IStatus<Avatar> SearchForCruisableAvatars()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Reactions

        // should maybe an action????
        public void DeletedEntity(DateTimeOffset deletedTimestamp)
        {
            throw new NotImplementedException();
        }

        public void WasCruisedBy(Avatar cruisee)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Helper Methods

        /// <summary>
        /// Gets the currenly active cruise if there is one.  
        /// </summary>
        private Cruise GetActiveCruise()
        {
            if (!HasLastCruise)
                return null;

            if (LastCruise.HasTimeRemaining())
                return LastCruise;

            return null;
        }

        /// <summary>
        /// Adds an error to a Status if Is Blindfold rule is violated. 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private Status<Avatar> IsBlindfolded(Status<Avatar> response)
        {
            if (!Blindfolded)
            {
                response.AddError
                    ("Cannot don or doff blindfold for an avatar if you are " +
                    "not wearing a Blindfold.");
            }
            return response;
        }

        private Status<Avatar> IsHooded(Status<Avatar> response)
        {
            if (!Hooded)
            {
                response.AddError
                    ("Cannot don or doff hood for an avatar if you are " +
                    "not wearing a hood.");
            }
            return response;
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



            if (cruises == null)
            {
                yield return new HankiesRuleViolation
                    ("One avatar owns many Cruises.", cruises);
            }
            else if (LastCruise == null)
            {
                yield return new HankiesRuleViolation
                    ("Every avatar has at least one session.", LastCruise);
            }

            // Rule: Avatars are immutable exception for the Session aggregate.
            // Rule: “Editing” an avatar creates a clone with the changed properties.

            yield break;
        }

        public bool Equals([AllowNull] Avatar x, [AllowNull] Avatar y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Avatar obj)
        {
            throw new NotImplementedException();
        }

        #endregion 
    }
}
