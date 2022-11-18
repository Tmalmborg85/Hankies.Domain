using System;
using Hankies.Domain.HankyCode.Appearance;

namespace Hankies.Domain.HankyCode.Flag
{
    public class CorduroyHanky : Flag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public override string Description => Color.Name + " corduroy";

        public CorduroyHanky(NamedColor SolidColor)
        {
            Color = SolidColor;
        }
    }
}
