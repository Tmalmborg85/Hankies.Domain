using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
	public class FlagJSONModel
	{
        public Guid ID { get; set; }

        public string Type { get; set; }

        public AssociatedTrait Trait { get; set; }

        public BaseAppearance Appearance { get; set; }

		public string VisualDescription { get; set; }

        public FlagJSONModel()
		{
		}
	}
}

