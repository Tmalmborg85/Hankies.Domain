using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Lame : SolidColorFabric
    {
        public override string FabricType => "lame";

        public Lame(NamedColor color) : base(color) { }
    }
}
