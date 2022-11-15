using System;

namespace Hankies.Domain.HankyCode.Appearance
{
    public class ColorWithStripe : Cotton
    {
        public NamedColor StripeColor { get; private set; }

        public override string Description => Color.Name + " with " + StripeColor.LowercaseName + " stripe";

        public ColorWithStripe(NamedColor solidColor, NamedColor stripeColor) : base(solidColor)
        {
            StripeColor = stripeColor;
        }

       
    }
}
