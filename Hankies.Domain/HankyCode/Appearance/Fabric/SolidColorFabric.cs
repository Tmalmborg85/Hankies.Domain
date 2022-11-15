using System;
namespace Hankies.Domain.HankyCode.Appearance.Fabric
{
    public abstract class SolidColorFabric : Appearance, ISolidColor
    {
        public NamedColor color;
        public NamedColor Color { get { return color; } }

        public abstract string FabricType { get; }

        public override string Description => Color.Name + " " + FabricType.ToLower();

        protected SolidColorFabric(NamedColor solidColor)
        {
            color = solidColor;
        }
    }
}
