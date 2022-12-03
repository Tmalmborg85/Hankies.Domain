using System;
using Hankies.Domain.HankyCode.Flag;

namespace Hankies.Domain.HankyCode.Fetish
{
	public class FlaggedRoll
	{
		public string Roll { get; set; }
		public FlaggableLocations FlagPosition { get; set; }

		public FlaggedRoll(FlaggableLocations flagPos, string roll)
		{
			Roll = roll;
			FlagPosition = flagPos;
		}
	}
}

