using System;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
	public class FlagModel
	{
        public Guid ID { get; set; }

        public AssociatedTrait Trait { get; set; }

        public string VisualDescription { get; set; }

        public FlagModel()
		{
		}
	}
}

