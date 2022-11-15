using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public abstract class Stripe : ISolidColor
    {
        public NamedColor Color { get; set; }
        public abstract string Description { get; }
    }
}
