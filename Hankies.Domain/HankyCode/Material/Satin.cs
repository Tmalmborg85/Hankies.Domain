using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Satin : Material
    {
        public override string FabricType => "satin";

        public Satin(NamedColor color) : base(color) { }
    }
}
