using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Details.DomainEvents;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.Radar
{
    /// <summary>
    /// A radar used to look for other IEchos (avatars) in an area. 
    /// </summary>
    public class CruiseRadar 
    {
        public CruiseRadar(Cruise ownedBy, IEnumerable<Customer> blockedCustomers)
        {
            Owner = ownedBy;
        }
        #region Properties
        /// <summary>
        /// Detected objects keyed into a dictiunary by thier EchoID
        /// </summary>
        private Dictionary<Guid, Cruise> _contacts { get; }

        /// <summary>
        /// IAvatars the radar has picked up. 
        /// </summary>
        /// <remarks>
        /// In the context of maritime radar the term Contact means any echo
        /// detected on the radarscope not evaluated as clutter or as a false
        /// echo.</remarks>
        public IEnumerable<Cruise> Contacts => _contacts?.Values
            .ToList();

        /// <summary>
        /// Objects flagged as clutter keyed into a dicinary by EchoID
        /// </summary>
        private Dictionary<Guid, Cruise> _clutter { get; }

        /// <summary>
        /// IAvatars that dont match enouch factors to be considerd Contacts. 
        /// </summary>
        /// <remarks>
        /// In the context of actual radar "Clutter" refers to radio frequency
        /// (RF) echoes returned from targets which are uninteresting to the
        /// radar operators.
        /// </remarks>
        public IEnumerable<Cruise> Clutter => _clutter?.Values
            .ToList();

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
        /// Evatulate a single new echo as Clutter or Contact.
        /// </summary>
        /// <param name="echo"></param>
        /// <remarks>
        /// 1. Cruise’s owning customer can not be blocked by my cruises’s
        ///     owning customer.
        /// 2. I can’t be blocked by echo’s owning customer.
        /// 3. Cruises must be valid
        /// 4. Cruises can’t already be in the my Radar.
        /// 5. Cruise's Avatar can’t have handkerchiefs in my hard pass handkerchiefs collection.
        /// 6. Cruise can’t be clutter 
        /// 8. match one of my handkerchiefs.
        /// </remarks>
        public void EvaluateNewEcho(RadarEcho echo)
        {
            // Already in radar, stop evaluating as new echo.
            if (_clutter.ContainsKey(echo.Source.EchoID) ||
                _contacts.ContainsKey(echo.Source.EchoID))
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

            if(EchoMatchesNoneOfMyHandkerchiefs(echo.Source))
            {
                FlagAsClutter(echo.Source);
                return;
            }
        }

        private bool EchoMatchesNoneOfMyHandkerchiefs(Cruise echo)
        {
            echo.CruisingAsAvatar.HasAnyCo
        }

        /// <summary>
        /// Checks if a Cruise's Avatar has hard passed on any of my Avatars
        /// handkerchiefs. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool EchoHardPassedOnAnyOfMyHandkerchiefs(Cruise echo)
        {
            return echo.CruisingAsAvatar.HardPassedOnAnyOfTheseHandkerchiefs
                (Owner.CruisingAsAvatar.Handkerchiefs);
        }

        /// <summary>
        /// Checks if I am blocked by customer who created the echo. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool IAmBlockedByEchoOwner(Cruise echo)
        {
            return echo.CruisingAsAvatar.CreatedByCustomer.HasBlocked
                (Owner.CruisingAsAvatar.CreatedByCustomer);
        }

        /// <summary>
        /// Checks if the customer who created this cruise is blocked by me. 
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        private bool EchoOwnerIsOnMyBlockedList(Cruise echo)
        {
            return Owner.CruisingAsAvatar.CreatedByCustomer.HasBlocked
                (echo.CruisingAsAvatar.CreatedByCustomer);
        }

        /// <summary>
        /// Evaluate many Echos as Clutter or Contacts.
        /// </summary>
        /// <param name="echos">The echos to evaluate.</param>
        public void EvaluateEchos(IEnumerable<Cruise> echos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Manualy flag an avatar as clutter for this radar, skipping the
        /// normal EvaluateEcho method.
        /// </summary>
        /// <param name="detectedObject">The object to be flagged</param>
        /// <returns>A status.</returns>
        /// <remarks>
        /// This could be used in case an Avatar later decides an IAvatar is
        /// clutter. If the IAvatar matches any avatars in Contacts, they
        /// should be removed. </remarks>
        public IStatus<CruiseRadar> FlagAsClutter(IRadarDetectable detectedObject)
        {
            var response = new Status<CruiseRadar>();

            var clutterKey = detectedObject.EchoID;
            var clutterValue = detectedObject;

            if (detectedObject == null)
                response.AddError("Object to flag as clutter cannot be null");

            if (clutterKey == null)
                response.AddError("Clutter must have a GUID key to be added " +
                    "to clutter the collection");

            if (!_clutter.ContainsKey(clutterKey))
            {
                _clutter.Add(clutterKey, clutterValue);

                RemoveClutterFromContact(clutterKey);
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
            (IEnumerable<IRadarDetectable> detectedObjects)
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

        private void RemoveClutterFromContact(Guid clutterKey)
        {
            if (_contacts.ContainsKey(clutterKey))
                _contacts.Remove(clutterKey);
        }
    }
}
