using System;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
	public class ItemFlag : BaseFlag
	{
        public string Item { get; set; }
        public ItemFlag(string item, AssociatedTrait trait) : base(item, trait)
		{
            SetVisualDescription(item);
            Item = item;
        }
    }
}

