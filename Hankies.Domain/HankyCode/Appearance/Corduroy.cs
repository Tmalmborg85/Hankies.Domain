using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class Corduroy : SolidColorFabric
    {
        public override string FabricType => "corduroy";

        public Corduroy(NamedColor color) : base(color) { }
    }
}
