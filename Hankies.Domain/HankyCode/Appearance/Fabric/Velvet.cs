using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public class Velvet : SolidColorFabric
    {
        public override string FabricType => "velvet";

        public Velvet(NamedColor solidColor) : base (solidColor)
        {
        }
    }
}
