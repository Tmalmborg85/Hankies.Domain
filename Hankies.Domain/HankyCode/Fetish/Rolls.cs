using System;
using System.Collections.Generic;

namespace Hankies.Domain.HankyCode.Fetish
{
	public static class Rolls
	{
		public static List<FlaggedRoll> TopBottomRolls => new List<FlaggedRoll>()
			{
				new FlaggedRoll(Flag.FlaggableLocations.Left,
				"Top"),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                "Bottom"),
            };

        public static List<FlaggedRoll> DomSubRolls => new List<FlaggedRoll>()
            {
                new FlaggedRoll(Flag.FlaggableLocations.Left,
                "Dom"),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                "Sub"),
            };

        public static List<FlaggedRoll> IsLikesRolls => new List<FlaggedRoll>()
            {
                new FlaggedRoll(Flag.FlaggableLocations.Left,
                "Is"),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                "Likes"),
            };

        public static List<FlaggedRoll> IntoNotIntoRolls => new List<FlaggedRoll>()
            {
                new FlaggedRoll(Flag.FlaggableLocations.Left,
                "Into"),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                "Not into"),
            };

        public static List<FlaggedRoll> IsIsIntoRolls => new List<FlaggedRoll>()
            {
                new FlaggedRoll(Flag.FlaggableLocations.Left,
                "Is"),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                "Into"),
            };

        public static List<FlaggedRoll> CustomRolls(string leftRoll, string rightRoll)
        {
            return new List<FlaggedRoll>()
            {
                new FlaggedRoll(Flag.FlaggableLocations.Left,
                leftRoll),
                new FlaggedRoll(Flag.FlaggableLocations.Right,
                rightRoll),
            };
        }

    }
}

