using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class VerticalStripe : Stripe
    {
        
        public VerticalStripe(NamedColor color)
        {
            Color = color;
        }

        public override string Description => "vertical " + Color.Name.ToLower() + " stripe";
    }
}
