using System;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public class SatinHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public override string Description => Color.Name + " satin";

        public SatinHanky(NamedColor SolidColor)
        {
            Color = SolidColor;
        }
    }
}
