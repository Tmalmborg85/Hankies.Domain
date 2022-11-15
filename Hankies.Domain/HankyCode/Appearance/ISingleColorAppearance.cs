using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public interface ISingleColorAppearance
    {
        public INamedColor Color { get; set; }
    }
}
