using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public class LaceHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public LaceHanky(NamedColor SolidColor, AssociatedTrait trait) : base(SolidColor.Name + "lace", trait)
        {
            Color = SolidColor;
            SetVisualDescription(SolidColor.Name + " lace");
        }
    }
}
