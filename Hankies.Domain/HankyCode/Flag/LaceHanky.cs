using System;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public class LaceHanky : Flag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public override string Description => Color.Name + " lace";

        public LaceHanky(NamedColor SolidColor)
        {
            Color = SolidColor;
        }
    }
}
