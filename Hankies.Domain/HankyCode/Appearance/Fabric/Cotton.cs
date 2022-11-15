using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public class Cotton : SolidColorFabric
    {
        public override string FabricType => "cotton";
        public override string Description => Color.Name;

        public Cotton(NamedColor color) : base(color) { }
    }
}
