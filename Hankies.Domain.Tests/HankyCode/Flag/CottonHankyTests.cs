using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class CottonHankyTests
    {
        public List<BaseFlag> GetAllKnownFlags()
        {
            var blackHanky = new CottonHanky(new SolidColor(ColorWheel.Black));
            var greyHanky = new CottonHanky(new SolidColor(ColorWheel.Grey));
            var blackWWhiteCheckHanky = new CottonHanky(new Checkerboard(ColorWheel.Black, ColorWheel.White));
            var greyWBlackHanky = new CottonHanky(new DuoColor(ColorWheel.Grey, ColorWheel.Black));
            var greyFlannelHanky = new CottonHanky(new Flannel(ColorWheel.Grey));
            var blackWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Black, ColorWheel.White));
            var charcoalHanky = new CottonHanky(new SolidColor(ColorWheel.Charcoal));
            var blackVelvetHanky = new VelvetHanky(ColorWheel.Black);
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue));
            var lightBlueWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.LightBlue, ColorWheel.White));
            var lightBlueWWhiteDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.White));
            var lightBlueWBlackDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Black));
            var lightBlueWBrownDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Brown));
            var lightBlueWYellowDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Yellow));
            var robinsEggBlueHanky = new CottonHanky(new SolidColor(ColorWheel.RobinEggBlue));
            var mediumBlueHanky = new CottonHanky(new SolidColor(ColorWheel.MediumBlue));
            var navyBlueHanky = new CottonHanky(new SolidColor(ColorWheel.NavyBlue));
            var airForceBlueHanky = new CottonHanky(new SolidColor(ColorWheel.AirForceBlue));
            var tealBlueHanky = new CottonHanky(new SolidColor(ColorWheel.TealBlue));
            var redHanky = new CottonHanky(new SolidColor(ColorWheel.Red));
            var redWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Red, ColorWheel.White));
            var redWBlackStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Red, ColorWheel.Black));
            var redGinghamHanky = new CottonHanky(new Gingham(ColorWheel.Red));
            var maroonHanky = new CottonHanky(new SolidColor(ColorWheel.Maroon));
            var darkRedHanky = new CottonHanky(new SolidColor(ColorWheel.DarkRed));
            var lightPinkHanky = new CottonHanky(new SolidColor(ColorWheel.LightPink));
            var darkPinkHanky = new CottonHanky(new SolidColor(ColorWheel.DarkPink));

            var magentaHanky = new CottonHanky(new SolidColor(ColorWheel.Magenta));
            var purpleHanky = new CottonHanky(new SolidColor(ColorWheel.Purple));
            var lavenderHanky = new CottonHanky(new SolidColor(ColorWheel.Lavender));
            var yellowHanky = new CottonHanky(new SolidColor(ColorWheel.Yellow));
            var paleYellowHanky = new CottonHanky(new SolidColor(ColorWheel.PaleYellow));
            var mustardHanky = new CottonHanky(new SolidColor(ColorWheel.Mustard));
            var goldHanky = new CottonHanky(new SolidColor(ColorWheel.Gold));
            var yellowWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Yellow, ColorWheel.White));
            var goldLameHanky = new LameHanky(ColorWheel.Gold);
            var orangeHanky = new CottonHanky(new SolidColor(ColorWheel.Orange));
            var apricotHanky = new CottonHanky(new SolidColor(ColorWheel.Apricot));
            var coralHanky = new CottonHanky(new SolidColor(ColorWheel.Coral));
            var rustHanky = new CottonHanky(new SolidColor(ColorWheel.Rust));
            var kellyGreenHanky = new CottonHanky(new SolidColor(ColorWheel.KellyGreen));
            var hunterGreenHanky = new CottonHanky(new SolidColor(ColorWheel.HunterGreen));
            var oliveDrabHanky = new CottonHanky(new SolidColor(ColorWheel.OliveDrab));
            var limeGreenHanky = new CottonHanky(new SolidColor(ColorWheel.LimeGreen));
            var beigeHanky = new CottonHanky(new SolidColor(ColorWheel.Beige));
            var brownHanky = new CottonHanky(new SolidColor(ColorWheel.Brown));
            var brownLaceHanky = new LaceHanky(ColorWheel.Brown);
            var brownSatinHanky = new SatinHanky(ColorWheel.Brown);
            var brownCorduroyHanky = new CorduroyHanky(ColorWheel.Brown);
            var brownWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Brown, ColorWheel.White));
            var furHanky = new Fur();
            var whiteHanky = new CottonHanky(new SolidColor(ColorWheel.White));
            //Holstein?? Milker
            var creamHanky = new CottonHanky(new SolidColor(ColorWheel.Cream));
            var whiteLaceHanky = new LaceHanky(ColorWheel.White);

            var whiteVelvetHanky = new VelvetHanky(ColorWheel.White);
            var whiteWMultiColorDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.White, ColorWheel.Multicolor));
            var silverLameHanky = new LameHanky(ColorWheel.Silver);
            var leopardHanky = new CottonHanky(new Leopard());
            var tanHanky = new CottonHanky(new SolidColor(ColorWheel.Tan));
            var teddybearFlag = new TeddyBear();
            //paisley?
            //unionjack? - do I want that in the app
            var mosquitoNettingFlag = new MosquitoNetting();
            var diryJockstrapFlag = new DirtyJockstrap();
            var doilyFlag = new Doily();
            //ziplock bag
            var cocktailNapkinFlag = new CocktailNapkin();
            var tissueFlag = new Tissue();
            var keyFlag = new Keys(); //NEEDS WORK can be flagged in 4 places. maybe house keys & car keys
            var terryclothFlag = new TerryCloth();

            var flags = new List<BaseFlag>();
            flags.Add(blackHanky);
            flags.Add(greyHanky);
            flags.Add(blackWWhiteCheckHanky);
            flags.Add(greyWBlackHanky);
            flags.Add(greyFlannelHanky);
            flags.Add(blackWWhiteStripeHanky);
            flags.Add(charcoalHanky);
            flags.Add(blackVelvetHanky);
            flags.Add(lightBlueHanky);
            flags.Add(lightBlueWWhiteStripeHanky);
            flags.Add(lightBlueWWhiteDotsHanky);
            flags.Add(lightBlueWBlackDotsHanky);
            flags.Add(lightBlueWBrownDotsHanky);
            flags.Add(lightBlueWYellowDotsHanky);
            flags.Add(robinsEggBlueHanky);
            flags.Add(mediumBlueHanky);
            flags.Add(navyBlueHanky);
            flags.Add(airForceBlueHanky);
            flags.Add(tealBlueHanky);
            flags.Add(redHanky);
            flags.Add(redWWhiteStripeHanky);
            flags.Add(redWBlackStripeHanky);
            flags.Add(redGinghamHanky);
            flags.Add(maroonHanky);
            flags.Add(darkRedHanky);
            flags.Add(lightPinkHanky);
            flags.Add(darkPinkHanky);

            flags.Add(magentaHanky);
            flags.Add(purpleHanky);
            flags.Add(lavenderHanky);
            flags.Add(yellowHanky);
            flags.Add(paleYellowHanky);
            flags.Add(mustardHanky);
            flags.Add(goldHanky);
            flags.Add(yellowWWhiteStripeHanky);
            flags.Add(goldLameHanky);
            flags.Add(orangeHanky);
            flags.Add(apricotHanky);
            flags.Add(coralHanky);
            flags.Add(rustHanky);
            flags.Add(kellyGreenHanky);
            flags.Add(hunterGreenHanky);
            flags.Add(oliveDrabHanky);
            flags.Add(limeGreenHanky);
            flags.Add(beigeHanky);
            flags.Add(brownHanky);
            flags.Add(brownLaceHanky);
            flags.Add(brownSatinHanky);
            flags.Add(brownCorduroyHanky);
            flags.Add(brownWWhiteStripeHanky);
            flags.Add(furHanky);
            flags.Add(whiteHanky);
            //flags.Add();
            flags.Add(creamHanky);
            flags.Add(whiteLaceHanky);

            flags.Add(whiteVelvetHanky);
            flags.Add(whiteWMultiColorDotsHanky);
            flags.Add(silverLameHanky);
            flags.Add(leopardHanky);
            flags.Add(tanHanky);
            flags.Add(teddybearFlag);
            //flags.Add();
            //flags.Add();
            flags.Add(mosquitoNettingFlag);
            flags.Add(diryJockstrapFlag);
            flags.Add(doilyFlag);
            //flags.Add();
            flags.Add(cocktailNapkinFlag);
            flags.Add(tissueFlag);
            flags.Add(keyFlag);
            flags.Add(terryclothFlag);
            flags.Add(navyBlueHanky);
            return flags;

        }
        [TestMethod]
        public void FlagsHaveUniqueIDS()
        {
            //Arrange
            var flags = GetAllKnownFlags();
            HashSet<Guid> IDs = new HashSet<Guid>();

            foreach (var flag in flags)
            {
                Assert.IsFalse(IDs.Contains(flag.ID));
                IDs.Add(flag.ID);
            }
        }
    }
}
