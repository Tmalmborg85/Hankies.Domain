using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Rules;

namespace Hankies.Domain.Details.DomainEntities
{
    public class Avatar : DomainEntity, IAvatar
    {
        private Avatar() { }

        public Avatar(Guid id, DateTimeOffset createdAt, Customer owner,
            string handle) : base
            (id, createdAt)
        {
            // Newly created avatars automatically start cruising.
            CreatedByCustomer = owner;
            Handle = handle;

            OnValidate();
        }

        public Customer CreatedByCustomer { get; private set; }

        /// <summary>
        /// Hashtable backing. 
        /// </summary>
        private HashSet<CruiseSession> sessions;

        /// <inheritdoc cref="IAvatar.Sessions"/>
        public IEnumerable<ICruisee>> Sessions => sessions?
            .ToList();

        public ICruisee LastSession =>
            sessions.OrderBy((ICruisee arg) => arg.StartedAt)
            .FirstOrDefault();

        public string Handle { get; private set; }

        public bool Blindfolded { get; private set; }

        public bool Hooded { get; private set; }

        public IPhoto ImpressionPhoto { get; private set; }

        public string ImpressionDescription { get; private set; }

        /// <summary>
        /// Hash set of all photos this customer owns
        /// </summary>
        private HashSet<CustomerPhoto> photos;

        public IEnumerable<IPhoto> ExposingPhotos => photos?
            .Where((IPhoto arg) => arg.Rating == PhotoRatings.NSFW)
            .ToList();

        public IEnumerable<IPhoto> SafeForWorkPhotos => photos?
            .Where((IPhoto arg) => arg.Rating == PhotoRatings.SFW)
            .ToList();

        private HashSet<Handkerchief> handkerchiefs;

        public IEnumerable<Handkerchief> Handkerchiefs => handkerchiefs.ToList();

        public IEnumerable<Handkerchief> LeftPocket => handkerchiefs
            .Where((Handkerchief hanky) => hanky.InPockets.Contains(PocketTypes.Left))
            .ToList();

        public IEnumerable<Handkerchief> RightPocket => handkerchiefs
            .Where((Handkerchief hanky) => hanky.InPockets.Contains(PocketTypes.Right))
            .ToList();

        public DateTimeOffset? DeletedAt { get; private set; }

        public bool Deleted { get; private set; }

        public bool HasActiveCruiseSession => throw new NotImplementedException();

        public IEnumerable<ICruise> Sessions => throw new NotImplementedException();

        ICruise IAvatar.LastSession => throw new NotImplementedException();

        public bool CanISeeThem(IAvatar them)
        {
            // If I am blindfolded I can see no one.
            if (Blindfolded)
            {
                // But maybe I took my blinfold off for them in this session.
                if (LastSession.BlindfoldRemovedFor.Contains(them))
                {
                    return true;
                }
                else
                {
                    // I am blinfolded and they do not have exception
                    return false;
                }
            }

            // If they are hooded I can not see them.
            if (them.Hooded)
            {
                //But maybe they removed thier hood for me.
                if (them.LastSession.HoodRemovedFor.Contains(this))
                {
                    return true;
                }
                else
                {
                    // I am hooded and they do not have an excpetion. 
                    return false;
                }
            }

            // nothing is blocking them from seeing me.
            return true;
        }

        public bool CanTheySeeMe(IAvatar they)
        {
            return they.CanISeeThem(this);
        }

        public bool HardPassedOnHandkerchied(Handkerchief handkerchief)
        {

        }

        public bool HardPassedOnAnyOfTheseHandkerchiefs
            (IEnumerable<Handkerchief> handkerchiefs)
        {

        }

        public void CruiseAnAvatar(IAvatar cruisee)
        {
            //LastSession.CruisedAvatars
            throw new NotImplementedException();
        }

        public DateTimeOffset CurrentExperation()
        {
            throw new NotImplementedException();
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
            if (CreatedByCustomer == null)
            {
                yield return new HankiesRuleViolation
                    ("Avatars are owned by one customer.", CreatedByCustomer);
            }
            else if (CreatedByCustomer.Avatars != null && CreatedByCustomer.Avatars.Contains(this))
            {
                yield return new HankiesRuleViolation
                    ("Avatars are distinct per customer.", CreatedByCustomer.Avatars);
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
