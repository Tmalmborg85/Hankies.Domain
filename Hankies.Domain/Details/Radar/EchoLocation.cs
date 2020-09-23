using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Hankies.Domain.Abstractions;
using Hankies.Domain.Abstractions.Radar;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;
using Hankies.Domain.Rules;

namespace Hankies.Domain.Details.Radar
{
    /// <summary>
    /// A validated location with a timestamp
    /// </summary>
    public class EchoLocation : IEchoLocation<EchoLocation>, IValidateable
    {
        #region constructors
        /// <summary>
        /// Private contructor for serialization and ORM
        /// </summary>
        private EchoLocation() { }

        /// <summary>
        /// Create a new location with a timestamp of now
        /// </summary>
        public EchoLocation(double lat, double lon)
        {
            Lattitude = lat;
            Longitude = lon;
            TimeStamp = DateTimeOffset.UtcNow;

            OnValidate();
        }

        public EchoLocation(double lat, double lon, DateTimeOffset timestamp)
        {
            Lattitude = lat;
            Longitude = lon;
            TimeStamp = timestamp;

            OnValidate();
        }
        #endregion
        #region Properties
        public DateTimeOffset TimeStamp { get; private set; }

        public double Lattitude { get; private set; }

        public double Longitude { get; private set; }
        #endregion
        #region Validation
        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }

        public IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            foreach (var violation in
                CoordinatesRules.GetCoordinatesRuleViolations(this))
            {
                yield return violation;
            }

            yield break;
        }

        #endregion

        /// <summary>
        /// Compares two locations lat/lon to see if they are the same. 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] EchoLocation other)
        {
            if (other == null)
                return false;

            return Lattitude == other.Lattitude &&
                Longitude == other.Longitude &&
                TimeStamp == TimeStamp;
        }

        public StalenessTypes Staleness()
        {
            return TimePassed.HowStale(TimeStamp);
        }

        public IStatus<EchoLocation> Update(ICoordinates coordinates)
        {
            var response = new Status<EchoLocation>();

            if (!this.IsValid)
                response.AddError("Can only update valid Echo Locations");

            foreach (var violation in CoordinatesRules.
                GetCoordinatesRuleViolations(coordinates))
            {
                response.AddError(violation.Rule);
            }

            // if there wernt errors go ahead and update this location. 
            if (response.IsSuccess())
            {
                Lattitude = coordinates.Lattitude;
                Longitude = coordinates.Longitude;
                TimeStamp = DateTimeOffset.UtcNow;
            }

            response.RespondWith(this);
            return response;
        }

        public IStatus<EchoLocation> Refresh()
        {
            var response = new Status<EchoLocation>();

            if (!this.IsValid)
                response.AddError("Can only refresh valid Echo Locations");

            TimeStamp = DateTimeOffset.UtcNow;

            response.RespondWith(this);

            return response;
        }
    }
}
