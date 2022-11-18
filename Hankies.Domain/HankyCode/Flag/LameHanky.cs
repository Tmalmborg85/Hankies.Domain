using System;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public class LameHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public override string Description => Color.Name + " lame";

        public LameHanky(NamedColor SolidColor)
        {
            Color = SolidColor;
        }
    }
}
