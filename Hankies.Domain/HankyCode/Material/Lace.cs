using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Lace : Material
    {
        public override string FabricType => "lace";

        public Lace(NamedColor color) : base(color) { }
    }
}
