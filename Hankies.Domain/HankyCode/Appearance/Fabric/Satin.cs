using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public class Satin : SolidColorFabric
    {
        public override string FabricType => "satin";

        public Satin(NamedColor color) : base(color) { }
    }
}
