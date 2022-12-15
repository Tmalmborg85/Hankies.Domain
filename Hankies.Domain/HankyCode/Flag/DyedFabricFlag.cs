using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using System.Drawing;

namespace Hankies.Domain.HankyCode.Flag
{
	public class DyedFabricFlag : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public DyedFabricFlag(NamedColor namedColor, string name,
            AssociatedTrait trait) : base(namedColor.LowercaseName + name, trait)
        {
            Color = namedColor;
            SetVisualDescription(namedColor.Name + " " + name);
        }
    }
}

