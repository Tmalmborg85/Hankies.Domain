using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class SolidColorWithCheck : Appearance, ISolidColor
    {
        public NamedColor Color { get; private set; }
        public NamedColor CheckColor { get; private set; }

        public override string Description => Color.Name + " with " + CheckColor.Name.ToLower() + " check";

        public SolidColorWithCheck(NamedColor solidColor, NamedColor checkColor)
        {
            Color = solidColor;
            CheckColor = checkColor;
        }
    }
}
