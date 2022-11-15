using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class Velvet : Appearance, ISolidColor
    {
        public NamedColor Color { get; private set; }

        public override string Description => Color.Name + " velvet";

        public Velvet(NamedColor solidColor)
        {
            Color = solidColor;
        }
    }
}
