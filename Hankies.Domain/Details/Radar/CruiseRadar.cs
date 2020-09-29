using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.Radar
{
    /// <summary>
    /// Tracks nearby Avatars that are crusing that may be of interest.  
    /// </summary>
    /// <remarks>
    /// A *CruiseRadar* is used to find nearby avatars that are cruising and
    /// correspond to at least some portions of my cruising avatar. Avatars on a
    /// CruiseRadar are added by various triggers and methods. It is up to the
    /// CruiseRadar to ensure Avatars added meet the required criteria.
    /// Conceptually the CruiseRadars are like real Radars so their properties
    /// and methods are named using maritime radar ubiquitous language as much
    /// as possible. 
    /// </remarks>
    public class CruiseRadar : ValidateableObject
    {
        const float MinRadarRange = 1.0f;
        const float MaxRadarRange = 10.0f;

        #region Constructors
        /// <summary>
        /// New radar with no pre exsisting data. 
        /// </summary>
        /// <param name="ownedBy">The cruise this radar belongs to.</param>
        /// <param name="range">How far this radar will scan from its
        /// centerpoint.</param>
        public CruiseRadar(Cruise ownedBy, float range)
        {
            if (_contacts == null) 
                _contacts = new HashSet<Cruise>();

            if (_clutter == null)
                _clutter = new HashSet<Cruise>();

            _pulses = new HashSet<RadarPulse>();

            Owner = ownedBy;
            Range = range;

            OnValidate();
        }

        /// <summary>
        /// New radar pre-populated with  clutter data. 
        /// </summary>
        /// <param name="ownedBy">The cruise this radar belongs to.</param>
        /// <param name="range">How far this radar will scan from its
        /// centerpoint.</param>
        /// <param name="clutter">Items this radar should ignore</param>
        public CruiseRadar(Cruise ownedBy, float range
            , HashSet<Cruise> clutter) : this(ownedBy, range)
        {
            _clutter = clutter;
        }

        /// <summary>
        /// Radar with pre-populated contacts and clutter data. 
        /// </summary>
        /// <param name="ownedBy">The cruise this radar belongs to.</param>
        /// <param name="range">How far this radar will scan from its
        /// centerpoint.</param>
        /// <param name="clutter">Items this radar should ignore</param>
        /// <param name="contacts">Items this radar has already contacted</param>
        public CruiseRadar(Cruise ownedBy, float range
            , HashSet<Cruise> clutter, HashSet<Cruise> contacts)
            : this(ownedBy, range)
        {
            _clutter = clutter;
            _contacts = contacts;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Unique cruise contacts this radar has that have not been flagged as
        /// clutter.  
        /// </summary>
        /// <remarks>
        /// This backing is a dictinary in place of a list so that O(1) lookups
        /// can be done for Contacts instead on O(N).
        /// </remarks>
        private HashSet<Cruise> _contacts { get; }

        /// <summary>
        /// Cruises the radar has picked up. 
        /// </summary>
        /// <remarks>
        /// In the context of maritime radar the term Contact means any echo
        /// detected on the radarscope not evaluated as clutter or as a false
        /// echo.</remarks>
        public IEnumerable<Cruise> Contacts => _contacts?.ToList();

        /// <summary>
        /// Objects flagged as clutter keyed into a dicinary by EchoID
        /// </summary>
        /// <remarks>
        /// This backing is a dictinary in place of a list so that O(1) lookups
        /// can be done for Contacts instead on O(N).
        /// </remarks>
        private HashSet<Cruise> _clutter { get; }

        /// <summary>
        /// IAvatars that dont match enouch factors to be considerd Contacts. 
        /// </summary>
        /// <remarks>
        /// In the context of actual radar "Clutter" refers to radio frequency
        /// (RF) echoes returned from targets which are uninteresting to the
        /// radar operators.
        /// </remarks>
        public IEnumerable<Cruise> Clutter => _clutter?.ToList();

        /// <summary>
        /// Pulses this radar has emited. 
        /// </summary>
        private HashSet<RadarPulse> _pulses { get; }

        /// <summary>
        /// Pulses this radar has emited. 
        /// </summary>
        public IEnumerable<RadarPulse> Pulses => _pulses?.ToList();

        /// <summary>
        /// The domain entity that owns this radar. Radars must be owned by a
        /// single domain entity. 
        /// </summary>
        public Cruise Owner { get; private set; }

        /// <summary>
        /// How far out will this radars pulse's look for echos. 
        /// </summary>
        public float Range { get; private set; }

        #endregion

        #region Actions
        /// <summary>
        /// Emits a radar pulse via a PulseEmittedDomainEvent
        /// </summary>
        /// <param name="location">The center point of the pulse</param>
        /// <remarks>
        /// Emitting a pulse triggers a PulseEmittedDomainEvent. Higher layers
        /// in Hankies handle the PulseEmittedDomainEvent and return a
        /// IRadarPulse to the radar object before saving to the database
        /// context.
        /// </remarks>
        public IStatus<PulseEmitedDomainEvent> EmitPulse(ICoordinates location)
        {
            var resultStatus = new Status<PulseEmitedDomainEvent>();
            var pulseEvent = new PulseEmitedDomainEvent(location.Lattitude,
                location.Longitude, Range, this);

            try
            {
                if (pulseEvent.IsValid)
                {
                    Owner.AddDomainEvent(pulseEvent);
                }
                else
                {
                    resultStatus.AddError("Invalid PulseEmitedDomainEvent, " +
                        "could not add to domain events");
                }
            }
            catch (Exception ex)
            {
                resultStatus.AddException(ex);
            }

            resultStatus.RespondWith(pulseEvent);
            return resultStatus;
        }

        /// <summary>
        /// Return an echo from a pulse this radar emited.  
        /// </summary>
        /// <param name="returnedEcho">The echo to return.</param>
        /// <returns></returns>
        public IStatus<CruiseRadar> ReturnEcho(RadarEcho returnedEcho)
        {
            var response = new Status<CruiseRadar>();

            try
            {
                if (_pulses.Contains(returnedEcho.OriginatingPulse))
                {
                    ContactOrClutter(returnedEcho);
                }
                else
                {
                    response.AddError
                        ("Echos can only be returned to the radars that " +
                        "emited the original pulse. ");
                }
            }
            catch (Exception ex)
            {
                response.AddException(ex);
            }

            response.RespondWith(this);
            return response;

        }

        /// <summary>
        /// Manualy flag an avatar as clutter for this radar, skipping the
        /// normal EvaluateEcho method.
        /// </summary>
        /// <param name="cruise">The object to be flagged</param>
        /// <returns>A status.</returns>
        /// <remarks>
        /// This could be used in case an Avatar later decides an IAvatar is
        /// clutter. If the IAvatar matches any avatars in Contacts, they
        /// should be removed. </remarks>
        public IStatus<CruiseRadar> FlagAsClutter(Cruise cruise)
        {
            var response = new Status<CruiseRadar>();

            if (cruise == null)
                response.AddError("Object to flag as clutter cannot be null");

            if (!_clutter.Contains(cruise))
            {
                _clutter.Add(cruise);
                RemoveClutterFromContact(cruise);
            }

            response.RespondWith(this);
            return response;
        }

        /// <summary>
        /// Manualy flag multiple avatars as clutter for this radar, skipping
        /// the normal EvaluateEchos method.
        /// </summary>
        /// <param name="detectedObjects">The objects to be flagged</param>
        /// <returns>A status.</returns>
        /// <remarks>
        /// This would be a good place to pre-flag blocked customers.
        /// </remarks>
        public IStatus<CruiseRadar> FlagAsClutter
            (IEnumerable<Cruise> detectedObjects)
        {
            var response = new Status<CruiseRadar>();

            if (detectedObjects == null)
                response.AddError("Objects to flag as clutter cannot be null");

            // Flag each object as clutter and record any errors. 
            foreach (var detectedObject in detectedObjects)
            {
                var flagedStatus = FlagAsClutter(detectedObject);
                if (!flagedStatus.IsSuccess())
                {
                    response.AddError(flagedStatus.ErrorMessage);
                }
            }

            response.RespondWith(this);
            return response;
        }
        #endregion

        #region Helper Methods

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (Owner == null)
                yield return new HankiesRuleViolation
                    ("Radars must be owned by a non null cruise", "Owner");

            if (Range < MinRadarRange)
                yield return new HankiesRuleViolation
                    ("Radar range must be greater than " + MinRadarRange, Range);

            if (Range > MinRadarRange)
                yield return new HankiesRuleViolation
                    ("Radar range must be less than " + MaxRadarRange, Range);

            if (Pulses == null)
                yield return new HankiesRuleViolation
                    ("Radars must have a non null pulse collection", _pulses);

            if (Contacts == null)
                yield return new HankiesRuleViolation
                    ("Radars must have a non null contacts collection",
                    _contacts);

            if (Clutter == null)
                yield return new HankiesRuleViolation
                    ("Radars must have a non null clutter collection",
                    _clutter);

        }


        /// <summary>
        /// Evatulate a single new echo as Clutter or Contact.
        /// </summary>
        /// <param name="echo"></param>
        /// <remarks>
        /// 1. Cruise’s owning customer can not be blocked by my cruises’s
        ///     owning customer.
        /// 2. I can’t be blocked by echo’s owning customer.
        /// 3. Cruises must be valid
        /// 4. Cruises can’t already be in the my Radar.
        /// 5. Cruise's Avatar can’t have handkerchiefs in my hard pass
        /// handkerchiefs collection.
        /// 6. Cruise can’t be clutter 
        /// 8. match one of my handkerchiefs.
        /// </remarks>
        private void ContactOrClutter(RadarEcho echo)
        {
            // Already in clutter, stop evaluating as new echo.
            if (_clutter.Contains(echo.Source))
                return;

            if (EchoOwnerIsOnMyBlockedList(echo.Source))
            {
                FlagAsClutter(echo.Source);
                return;
            }

            if (IAmBlockedByEchoOwner(echo.Source))
            {
                FlagAsClutter(echo.Source);
                return;
            }

            if (!echo.IsValid)
            {
                FlagAsClutter(echo.Source);
                return;
            }

            if (EchoHardPassedOnAnyOfMyHandkerchiefs(echo.Source))
            {
                FlagAsClutter(echo.Source);
                return;
            }
            
        }

        /// <summary>
        /// Checks if a Cruise's Avatar has hard passed on any of my Avatars
        /// handkerchiefs. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool EchoHardPassedOnAnyOfMyHandkerchiefs(Cruise echo)
        {
            return echo.Avatar.HardPassedOnAnyOfTheseHandkerchiefs
                (Owner.Avatar.Handkerchiefs);
        }

        /// <summary>
        /// Checks if I am blocked by customer who created the echo. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool IAmBlockedByEchoOwner(Cruise echo)
        {
            return echo.Avatar.Customer.HasBlocked
                (Owner.Avatar.Customer);
        }

        /// <summary>
        /// Is the customer who owns this cruise blocked by me. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool EchoOwnerIsOnMyBlockedList(Cruise echo)
        {
            return Owner.Avatar.Customer.HasBlocked
                (echo.Avatar.Customer);
        }

        /// <summary>
        /// Remove clutter from the contact list if it is there. 
        /// </summary>
        /// <param name="clutter"></param>
        private void RemoveClutterFromContact(Cruise clutter)
        {
            if (_contacts.Contains(clutter))
                _contacts.Remove(clutter);
        }

        #endregion
    }
}
