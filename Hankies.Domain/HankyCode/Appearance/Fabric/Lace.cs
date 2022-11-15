using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public class Lace : SolidColorFabric
    {
        public override string FabricType => "lace";

        public Lace(NamedColor color) : base(color) { }
    }
}
