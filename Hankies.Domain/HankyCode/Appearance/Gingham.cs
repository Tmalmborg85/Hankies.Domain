using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    /// <summary>
    /// One color weaved over white creates a check looking pattern. 
    /// </summary>
    public class Gingham : Checkerboard
    {
        public override string Description => Color.Name + " gingham";

        public Gingham(NamedColor solidColor) : base(solidColor, new NamedColor("White", "#fffffff"))
        {
        }
    }
}
