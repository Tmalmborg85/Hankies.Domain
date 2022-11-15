using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class SolidColorWithDots : Appearance, ISolidColor
    {
        public NamedColor Color { get; private set; }
        public NamedColor DotColor { get; private set; }

        public override string Description => Color.Name + " with " + DotColor.Name.ToLower() + " dots";

        public SolidColorWithDots(NamedColor solidColor, NamedColor dotColor)
        {
            Color = solidColor;
            DotColor = dotColor;
        }
    }
}
