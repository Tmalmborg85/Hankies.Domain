using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Rules
{
    /// <summary>
    /// Enforcement of lattitude and longitude rules. 
    /// </summary>
    public static class CoordinatesRules
    {
        /// <summary>
        /// Latitudes range from -90 to 90
        /// </summary>
        static int MinLattitudeRange = -90;

        /// <summary>
        /// Latitudes range from -90 to 90
        /// </summary>
        static int MaxLattitudeRange = 90;

        /// <summary>
        /// Longitudes range from -180 to 80.
        /// </summary>
        static int MinLongitudeRange = -180;

        /// <summary>
        /// Longitudes range from -180 to 80.
        /// </summary>
        static int MaxLongitudeRange = 80;

        /// <summary>
        /// Check for any rule violation in a set of coordinates. 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public static IEnumerable<HankiesRuleViolation> GetCoordinatesRuleViolations
            (ICoordinates coordinates)
        {
            if (coordinates.Longitude == 0 && coordinates.Lattitude == 0)
                yield return new HankiesRuleViolation("Latitude and Longitude " +
                    "both cant be 0.", coordinates);

            if (MinLattitudeRange <= coordinates.Lattitude &&
                coordinates.Lattitude <= MaxLattitudeRange)
                yield return new HankiesRuleViolation
                    ("Latitudes must range from -90 to 90", coordinates);

            if (MinLongitudeRange <= coordinates.Longitude &&
                coordinates.Longitude <= MaxLongitudeRange)
                yield return new HankiesRuleViolation
                    ("Longitudes range from -180 to 80.", coordinates);

            yield break;
        }
    }
}
