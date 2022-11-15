using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    /// <summary>
    /// A checkered pattern consists of squares in two different colours positioned in alternating positions. As its name suggests, think of a checkerboard or the finishing line flag at a Formula One race.
    /// </summary>
    public class Checkerboard : Cotton
    {
        public NamedColor CheckColor { get; private set; }

        public override string Description => Color.Name + " with " + CheckColor.Name.ToLower() + " check";

        public Checkerboard(NamedColor solidColor, NamedColor checkColor) : base (solidColor)
        {
            CheckColor = checkColor;
        }
    }
}
