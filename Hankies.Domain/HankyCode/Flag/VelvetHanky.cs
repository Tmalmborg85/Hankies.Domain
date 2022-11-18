using System;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public class VelvetHanky : Flag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public override string Description => Color.Name + " velvet";

        public VelvetHanky(NamedColor SolidColor)
        {
            Color = SolidColor;
        }
    }
}
