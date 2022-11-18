using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public static class ColorWheel
    {
        public static NamedColor White => new NamedColor("White", "#ffffff");
        public static NamedColor Black => new NamedColor("Black", "#000000");
        public static NamedColor Grey => new NamedColor("Grey", "#808080");
        public static NamedColor Charcoal = new NamedColor("Charcoal", "#36454f");
    }
}
