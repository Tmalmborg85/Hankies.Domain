using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class Flannel : SolidColor
    {
        public override string Description => Color.Name + " flannel";


        public Flannel(NamedColor color) : base(color)
        {
        }
    }
}
