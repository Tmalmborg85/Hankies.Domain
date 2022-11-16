using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Velvet : Material
    {
        public override string FabricType => "velvet";

        public Velvet(NamedColor solidColor) : base (solidColor)
        {
        }
    }
}
