using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    /// <summary>
    /// Cotton Hankies are the most common kind of hanky. Thier description never includes that they are made from cotton because that is assumed by default. Hankies that are made of other materials will say so. 
    /// </summary>
    public class CottonHanky : BaseFlag
    {
        public BaseAppearance Appearance { get; set; }


        public CottonHanky(BaseAppearance appearance, AssociatedTrait trait) : base(appearance.ID, trait)
        {
            Appearance = appearance;
            SetVisualDescription(appearance.Description);
        }
    }
}
