using System;
using System.Diagnostics;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public class LameHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public LameHanky(NamedColor SolidColor, AssociatedTrait trait) : base(SolidColor.Name + "lame", trait)
        {
            Color = SolidColor;
            SetVisualDescription(SolidColor.Name + " lame");
        }
    }
}
