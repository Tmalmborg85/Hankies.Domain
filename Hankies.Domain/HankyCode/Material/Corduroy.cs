using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Corduroy : Material
    {
        public override string FabricType => "corduroy";

        public Corduroy(NamedColor color) : base(color) { }
    }
}
