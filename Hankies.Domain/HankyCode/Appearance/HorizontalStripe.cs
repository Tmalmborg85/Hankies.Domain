using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class HorizontalStripe : Stripe
    {
        public HorizontalStripe(NamedColor color)
        {
            Color = color;
        }

        public override string Description => "horizontal " + Color.Name + " stripe";
    }
}
