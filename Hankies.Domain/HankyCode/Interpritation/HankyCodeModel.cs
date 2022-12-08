using System;
using Hankies.Domain.HankyCode.Flag;
using System.Collections.Generic;

namespace Hankies.Domain.HankyCode.Interpritation
{
	public class HankyCodeModel
	{
		public string Version { get; set; }
		public DateTimeOffset LastUpdated { get; set; }
		public Dictionary<Guid, RecomendedFlag> RecomendedFlags { get; set; }
        public Dictionary<Guid, DoffedFlag> DoffedFlags { get; set; }
	}
}

