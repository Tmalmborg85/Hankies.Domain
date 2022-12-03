using System;
using System.Diagnostics;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public class SatinHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public SatinHanky(NamedColor SolidColor, AssociatedTrait trait) : base(SolidColor.Name + "satin", trait)
        {
            Color = SolidColor;
            SetVisualDescription(SolidColor.Name + " satin");
        }
    }
}
