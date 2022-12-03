using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public class VelvetHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public VelvetHanky(NamedColor SolidColor, AssociatedTrait trait) : base (SolidColor.Name + "velvet", trait)
        {
            Color = SolidColor;
            SetVisualDescription(SolidColor.Name + " velvet");
        }
    }
}
