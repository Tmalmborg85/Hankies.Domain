using System;

namespace Hankies.Domain.HankyCode.Appearance
{
    public class ColorWithDots : SolidColor
    {
        public NamedColor DotColor { get; private set; }

        public override string Description => Color.Name + " with " + DotColor.Name.ToLower() + " dots";

        public ColorWithDots(NamedColor solidColor, NamedColor dotColor) : base(solidColor)
        {
            DotColor = dotColor;
        }
    }
}
