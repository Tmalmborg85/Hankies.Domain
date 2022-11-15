using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public class Lame : SolidColorFabric
    {
        public override string FabricType => "lame";

        public Lame(NamedColor color) : base(color) { }
    }
}
