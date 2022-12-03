using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public class CorduroyHanky : BaseFlag, ISolidColor
    {
        public NamedColor Color { get; set; }

        public CorduroyHanky(NamedColor SolidColor, AssociatedTrait trait) : base(SolidColor.Name + "corduroy", trait)
        {
            SetVisualDescription(SolidColor.Name + " corduroy");
            Color = SolidColor;
        }
    }
}
