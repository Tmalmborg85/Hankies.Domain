using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class DuoColor : BaseAppearance
    {
        public NamedColor ColorA { get; private set; }
        public NamedColor ColorB { get; private set; }

        public override string Description => ColorA.Name + " with " + ColorB.Name.ToLower();

        public DuoColor(NamedColor colorA, NamedColor colorB)
        {
            ColorA = colorA;
            ColorB = colorB;
        }
    }
}
