using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class SolidColorWithStripe : Appearance, ISolidColor
    {
        public NamedColor Color { get; private set; }
        public Stripe Stripe { get; private set; }

        public override string Description => Color.Name + " with " + Stripe.Description;

        public SolidColorWithStripe(NamedColor solidColor, Stripe stripe)
        {
            Color = solidColor;
            Stripe = stripe;
        }

       
    }
}
