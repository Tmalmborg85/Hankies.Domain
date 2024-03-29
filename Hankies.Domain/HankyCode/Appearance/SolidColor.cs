﻿using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    public class SolidColor : BaseAppearance, ISolidColor
    {
        public NamedColor Color { get; private set; }

        public SolidColor(NamedColor color)
        {
            Color = color;
        }

        public override string Description => Color.Name;
    }
}
