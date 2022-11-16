using System;
namespace Hankies.Domain.HankyCode.Material
{
    public class Cotton : Material
    {
        public override string FabricType => "cotton";
        public override string Description => Color.Name;

        public Cotton(NamedColor color) : base(color) { }
    }
}
