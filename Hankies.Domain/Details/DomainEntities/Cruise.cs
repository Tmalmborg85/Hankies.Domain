using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Details.DomainEvents.AvatarEvents;
using Hankies.Domain.Details.Radar;
using Hankies.Domain.Details.ValueObjects;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.DomainEntities
{
    /// <summary>
    /// An avatars representation at a range in time. 
    /// </summary>
    /// <remarks>
    /// Avatar sessions are part of the overall avatar domain objcet. As such
    /// this can be an enimic model with no or minimalactions. Most if not all
    /// actions should be done by the owning avatar. 
    /// </remarks>
    public class Cruise : DomainEntity
    {
        #region Constructors

        /// <summary>
        /// For serialization and ORMs
        /// </summary>
        private Cruise() { }

        public Cruise(Avatar avatar, EchoLocation startLocation)
        {
           
            Avatar = avatar;

            Locations = new List<EchoLocation>();
            Locations.Add(startLocation);

            if (StartedAt == null)
                StartedAt = DateTimeOffset.UtcNow;

            OnValidate();
        }
        #endregion

        #region Hashset Backings

        /// <summary>
        /// Avatars which have been exempt from any blindfold rules I have,
        /// for this cruise only. 
        /// </summary>
        private HashSet<Avatar> DoffedBlindfoldFor { get; set; }

        /// <summary>
        /// Avatars which have been exempt from my hood waering rules. 
        /// </summary>
        private HashSet<Avatar> DoffedHoodFor { get; set; }
        #endregion

        #region Parrent Only Actions

        /// <summary>
        /// Doff a blindfold for an individual for this cruise session only. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="forAvatar"></param>
        /// <returns></returns>
        internal Status<Avatar> DoffBlindfold(Status<Avatar> response,
            Avatar sender, Avatar forAvatar)
        {
            try
            {
                response = IsCorrectSender(response, sender);
                if (response.IsSuccess())
                {
                    DoffedBlindfoldFor.Add(forAvatar);

                    if (DoffedBlindfoldFor == null)
                        DoffedBlindfoldFor = new HashSet<Avatar>();
                }
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            return response;
        }

        /// <summary>
        /// Doff a hood for an avatar if you are wearing one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="forAvatar"></param>
        /// <returns></returns>
        internal Status<Avatar> DoffHood(Status<Avatar> response,
            Avatar sender, Avatar forAvatar)
        {
            try
            {
                response = IsCorrectSender(response, sender);
                if (response.IsSuccess())
                {
                    if (DoffedHoodFor == null)
                        DoffedHoodFor = new HashSet<Avatar>();

                    DoffedHoodFor.Add(forAvatar);
                }
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }
            return response;
        }

        internal Status<Avatar> DonBlindfold(Status<Avatar> response,
            Avatar sender, Avatar forAvatar)
        {
            try
            {
                response = IsCorrectSender(response, sender);
                if (response.IsSuccess())
                {
                    DoffedBlindfoldFor.Remove(forAvatar);
                }
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            return response;
        }

        internal Status<Avatar> DonHood(Status<Avatar> response,
            Avatar sender, Avatar forAvatar)
        {
            try
            {
                response = IsCorrectSender(response, sender);
                if (response.IsSuccess())
                {
                    DoffedHoodFor.Remove(forAvatar);
                }
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            return response;
        }

        internal Status<Avatar> ExtendTime(Status<Avatar> response, Avatar sender
            , TimeExtension extension)
        {
            try
            {
                response = IsCorrectSender(response, sender);
                if (response.IsSuccess())
                    TimeExtensions.Add(extension);
                
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            return response;
        }
        #endregion

        #region Properties
        /// <summary>
        /// When this session was started. Immutable.
        /// </summary>
        public DateTimeOffset StartedAt { get; }

        /// <summary>
        /// If/When this cruise was stopped
        /// </summary>
        public DateTimeOffset? StoppedAt { get; set; }

        /// <summary>
        /// How much time is left of this cruise. 
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// The Avatar this cruise is for. 
        /// </summary>
        public Avatar Avatar { get; private set; }

        /// <summary>
        /// ID that can be picked up by a <c cref="CruiseRadar">Cruise Radar</c>
        /// </summary>
        /// <remarks>
        /// Cruise EchoIDs should be the same as the cruises owning AvatarID.
        /// This ensures that echo flagging (clutter or contact) will persist
        /// in the cruise radars session
        /// </remarks>
        public Guid EchoID => Avatar.Id;

        public IList<EchoLocation> Locations { get; private set; }

        public EchoLocation MostRecentLocation => Locations.OrderBy
            (EL => EL.TimeStamp).FirstOrDefault();


        
        /// <summary>
        /// Contains a collection of nearby avatars that meet your criteria
        /// and tools to look for them
        /// </summary>
        CruiseRadar CruiseRadar { get; }

        
        /// <summary>
        /// Avatars that I have cruised. 
        /// </summary>
        IEnumerable<Avatar> Cruised { get; }

        /// <summary>
        /// Specific people the blind fold is off for. 
        /// </summary>
        /// <remarks>
        /// This only applies to avatars that are wearing a blindfold. 
        /// </remarks>
        IEnumerable<Avatar> BlindfoldRemovedFor { get; }

        /// <summary>
        /// Specific people the hood is off for. 
        /// </summary>
        /// <remarks>
        /// This only applies to avatars that are wearing a hood. 
        /// </remarks>
        IEnumerable<Avatar> HoodRemovedFor { get; }

        IList<TimeExtension> TimeExtensions { get; }
        #endregion

        #region Helper Methods

        private Status<Avatar> IsCorrectSender(Status<Avatar> response, Avatar sender)
        {
            if (Avatar != sender)
            {
                response.AddError
                    ("Only the owning avatar may modify its cruise.");
            }
            return response;
        }

        private int ExtendedByMinutes()
        {
            int minutes = 0;
            foreach (var extension in TimeExtensions)
            {
                minutes += extension.Minutes;
            }
            return minutes;
        }

        public bool HasTimeRemaining()
        {
            return TimeRemaining().TotalSeconds > 0;
        }

        public TimeSpan TimeRemaining()
        {
            var cruiseEndTime = StartedAt.AddMinutes(Time.TotalMinutes);
            cruiseEndTime.AddMinutes(ExtendedByMinutes());

            return cruiseEndTime.Subtract(DateTimeOffset.UtcNow);
        }

        public bool IsBlindfoldRemovedForAvatar(Avatar avatar)
        {
            if (DoffedBlindfoldFor == null)
                return false;

            return DoffedBlindfoldFor.Contains(avatar);
        }

        public bool IsHoodRemovedForAvatar(Avatar avatar)
        {
            if (DoffedHoodFor == null)
                return false;

            return DoffedHoodFor.Contains(avatar);
        }

        public bool Equals([AllowNull] IRadarDetectable other)
        {
            if (other == null)
                return false;

            return this.EchoID == other.EchoID;
        }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            throw new NotImplementedException();
        }

        

        #endregion
    }
}
