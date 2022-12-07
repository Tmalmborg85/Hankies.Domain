using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Hankies.Domain.HankyCode.Interpritation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode
{
	/// <summary>
	/// Use these tests to make sure hanky code translation works.
	/// <remarks>
	/// Using TTD (Test Driven Design) so theses tests will be written first.
	/// Start writing here and then create classes, and thier tests, as needed.
	/// By the end there should be a fully functioning Hanky Code Service. 
	/// </remarks>
	/// </summary>
	[TestClass]
	public class HankyCodeServiceTests
	{
		public HankyCodeService HankyCode { get; set; }

		public HankyCodeServiceTests()
		{
			//Create a default hanky code
			HankyCode = new HankyCodeService();

			PopulateHankyCodeWithBasicHankies();
		}

        /// <summary>
        /// Populate the testing <c>HankyCodeService</c> with several Hankies
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void PopulateHankyCodeWithBasicHankies()
        {
            {
                var blackHanky = new CottonHanky(new SolidColor(ColorWheel.Black),
                    new AssociatedTrait("Heavy Sadomasochism", Rolls.TopBottomRolls));
                var greyHanky = new CottonHanky(new SolidColor(ColorWheel.Grey),
                    new AssociatedTrait("Bondage", Rolls.TopBottomRolls));
                var blackWWhiteCheckHanky = new CottonHanky(new Checkerboard(ColorWheel.Black, ColorWheel.White),
                    new AssociatedTrait("Safe sex", Rolls.TopBottomRolls));
                var greyWBlackHanky = new CottonHanky(new DuoColor(ColorWheel.Grey, ColorWheel.Black),
                    new AssociatedTrait("Light Sadomasochism", Rolls.TopBottomRolls));
                var greyFlannelHanky = new CottonHanky(new Flannel(ColorWheel.Grey),
                    new AssociatedTrait("Suits", Rolls.CustomRolls("Wears suits", "Into people wearing suits")));
                var blackWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Black, ColorWheel.White),
                    new AssociatedTrait("Black", Rolls.CustomRolls("Likes black bottoms","Likes black tops")));
                var charcoalHanky = new CottonHanky(new SolidColor(ColorWheel.Charcoal),
                    new AssociatedTrait("Latex festish", Rolls.TopBottomRolls));
                var blackVelvetHanky = new VelvetHanky(ColorWheel.Black,
                    new AssociatedTrait("Video", Rolls.CustomRolls("Likes to take video","Likes to be on camera")));
                var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));
                //var lightBlueWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.LightBlue, ColorWheel.White));
                //var lightBlueWWhiteDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.White));
                //var lightBlueWBlackDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Black));
                //var lightBlueWBrownDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Brown));
                //var lightBlueWYellowDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.LightBlue, ColorWheel.Yellow));
                //var robinsEggBlueHanky = new CottonHanky(new SolidColor(ColorWheel.RobinEggBlue));
                //var mediumBlueHanky = new CottonHanky(new SolidColor(ColorWheel.MediumBlue));
                //var navyBlueHanky = new CottonHanky(new SolidColor(ColorWheel.NavyBlue));
                //var airForceBlueHanky = new CottonHanky(new SolidColor(ColorWheel.AirForceBlue));
                //var tealBlueHanky = new CottonHanky(new SolidColor(ColorWheel.TealBlue));
                //var redHanky = new CottonHanky(new SolidColor(ColorWheel.Red));
                //var redWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Red, ColorWheel.White));
                //var redWBlackStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Red, ColorWheel.Black));
                //var redGinghamHanky = new CottonHanky(new Gingham(ColorWheel.Red));
                //var maroonHanky = new CottonHanky(new SolidColor(ColorWheel.Maroon));
                //var darkRedHanky = new CottonHanky(new SolidColor(ColorWheel.DarkRed));
                //var lightPinkHanky = new CottonHanky(new SolidColor(ColorWheel.LightPink));
                //var darkPinkHanky = new CottonHanky(new SolidColor(ColorWheel.DarkPink));

                //var magentaHanky = new CottonHanky(new SolidColor(ColorWheel.Magenta));
                //var purpleHanky = new CottonHanky(new SolidColor(ColorWheel.Purple));
                //var lavenderHanky = new CottonHanky(new SolidColor(ColorWheel.Lavender));
                //var yellowHanky = new CottonHanky(new SolidColor(ColorWheel.Yellow));
                //var paleYellowHanky = new CottonHanky(new SolidColor(ColorWheel.PaleYellow));
                //var mustardHanky = new CottonHanky(new SolidColor(ColorWheel.Mustard));
                //var goldHanky = new CottonHanky(new SolidColor(ColorWheel.Gold));
                //var yellowWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Yellow, ColorWheel.White));
                //var goldLameHanky = new LameHanky(ColorWheel.Gold);
                var orangeHanky = new CottonHanky(new SolidColor(ColorWheel.Orange),
                    new AssociatedTrait("All or Nothing", Rolls.CustomRolls("Anything, Anytime", "Nothing now, just looking")));
                //var apricotHanky = new CottonHanky(new SolidColor(ColorWheel.Apricot));
                //var coralHanky = new CottonHanky(new SolidColor(ColorWheel.Coral));
                //var rustHanky = new CottonHanky(new SolidColor(ColorWheel.Rust));
                //var kellyGreenHanky = new CottonHanky(new SolidColor(ColorWheel.KellyGreen));
                //var hunterGreenHanky = new CottonHanky(new SolidColor(ColorWheel.HunterGreen));
                //var oliveDrabHanky = new CottonHanky(new SolidColor(ColorWheel.OliveDrab));
                //var limeGreenHanky = new CottonHanky(new SolidColor(ColorWheel.LimeGreen));
                //var beigeHanky = new CottonHanky(new SolidColor(ColorWheel.Beige));
                //var brownHanky = new CottonHanky(new SolidColor(ColorWheel.Brown));
                //var brownLaceHanky = new LaceHanky(ColorWheel.Brown);
                //var brownSatinHanky = new SatinHanky(ColorWheel.Brown);
                //var brownCorduroyHanky = new CorduroyHanky(ColorWheel.Brown);
                //var brownWWhiteStripeHanky = new CottonHanky(new ColorWithStripe(ColorWheel.Brown, ColorWheel.White));
                //var furHanky = new Fur();
                //var whiteHanky = new CottonHanky(new SolidColor(ColorWheel.White));
                ////Holstein?? Milker
                //var creamHanky = new CottonHanky(new SolidColor(ColorWheel.Cream));
                //var whiteLaceHanky = new LaceHanky(ColorWheel.White);

                //var whiteVelvetHanky = new VelvetHanky(ColorWheel.White);
                //var whiteWMultiColorDotsHanky = new CottonHanky(new ColorWithDots(ColorWheel.White, ColorWheel.Multicolor));
                //var silverLameHanky = new LameHanky(ColorWheel.Silver);
                //var leopardHanky = new CottonHanky(new Leopard());
                //var tanHanky = new CottonHanky(new SolidColor(ColorWheel.Tan));
                //var teddybearFlag = new TeddyBear();
                ////paisley?
                ////unionjack? - do I want that in the app
                //var mosquitoNettingFlag = new MosquitoNetting();
                //var diryJockstrapFlag = new DirtyJockstrap();
                //var doilyFlag = new Doily();
                ////ziplock bag
                //var cocktailNapkinFlag = new CocktailNapkin();
                //var tissueFlag = new Tissue();
                //var keyFlag = new Keys(); //NEEDS WORK can be flagged in 4 places. maybe house keys & car keys
                //var terryclothFlag = new TerryCloth();

                HankyCode.AddFlag(blackHanky);
                HankyCode.AddFlag(greyHanky);
                HankyCode.AddFlag(blackWWhiteCheckHanky);
                HankyCode.AddFlag(greyWBlackHanky);
                HankyCode.AddFlag(greyFlannelHanky);
                HankyCode.AddFlag(blackWWhiteStripeHanky);
                HankyCode.AddFlag(charcoalHanky);
                HankyCode.AddFlag(blackVelvetHanky);
                HankyCode.AddFlag(lightBlueHanky);

                HankyCode.AddFlag(orangeHanky);
                //flags.Add(lightBlueWWhiteStripeHanky);
                //flags.Add(lightBlueWWhiteDotsHanky);
                //flags.Add(lightBlueWBlackDotsHanky);
                //flags.Add(lightBlueWBrownDotsHanky);
                //flags.Add(lightBlueWYellowDotsHanky);
                //flags.Add(robinsEggBlueHanky);
                //flags.Add(mediumBlueHanky);
                //flags.Add(navyBlueHanky);
                //flags.Add(airForceBlueHanky);
                //flags.Add(tealBlueHanky);
                //flags.Add(redHanky);
                //flags.Add(redWWhiteStripeHanky);
                //flags.Add(redWBlackStripeHanky);
                //flags.Add(redGinghamHanky);
                //flags.Add(maroonHanky);
                //flags.Add(darkRedHanky);
                //flags.Add(lightPinkHanky);
                //flags.Add(darkPinkHanky);

                //flags.Add(magentaHanky);
                //flags.Add(purpleHanky);
                //flags.Add(lavenderHanky);
                //flags.Add(yellowHanky);
                //flags.Add(paleYellowHanky);
                //flags.Add(mustardHanky);
                //flags.Add(goldHanky);
                //flags.Add(yellowWWhiteStripeHanky);
                //flags.Add(goldLameHanky);
                //flags.Add(orangeHanky);
                //flags.Add(apricotHanky);
                //flags.Add(coralHanky);
                //flags.Add(rustHanky);
                //flags.Add(kellyGreenHanky);
                //flags.Add(hunterGreenHanky);
                //flags.Add(oliveDrabHanky);
                //flags.Add(limeGreenHanky);
                //flags.Add(beigeHanky);
                //flags.Add(brownHanky);
                //flags.Add(brownLaceHanky);
                //flags.Add(brownSatinHanky);
                //flags.Add(brownCorduroyHanky);
                //flags.Add(brownWWhiteStripeHanky);
                //flags.Add(furHanky);
                //flags.Add(whiteHanky);
                ////flags.Add();
                //flags.Add(creamHanky);
                //flags.Add(whiteLaceHanky);

                //flags.Add(whiteVelvetHanky);
                //flags.Add(whiteWMultiColorDotsHanky);
                //flags.Add(silverLameHanky);
                //flags.Add(leopardHanky);
                //flags.Add(tanHanky);
                //flags.Add(teddybearFlag);
                ////flags.Add();
                ////flags.Add();
                //flags.Add(mosquitoNettingFlag);
                //flags.Add(diryJockstrapFlag);
                //flags.Add(doilyFlag);
                ////flags.Add();
                //flags.Add(cocktailNapkinFlag);
                //flags.Add(tissueFlag);
                //flags.Add(keyFlag);
                //flags.Add(terryclothFlag);

                //return flags;

            }
        }


        /// <summary>
        /// Add a new undonned(not worn) flag to the HankyCode.  
        /// </summary>
        [TestMethod]
        public void AddNewFlagToHankyCode()
		{
            //Arrange
            //create a new flag with appearance
            var newFlag = new CottonHanky(
                new SolidColor(ColorWheel.RobinEggBlue),
                new AssociatedTrait("69", Rolls.IntoNotIntoRolls, "Mutual oral sex"));
                
            //add the flag to the hanky code.
            HankyCode.AddFlag(newFlag);

			//Assert
			Assert.IsTrue(HankyCode.HasFlag(newFlag));
        }

        /// <summary>
        /// Should fail to add a duplicate flag to the hanky code.   
        /// </summary>
        [TestMethod]
        public void FailToAddDuplicateFlagToHankyCode()
        {
            //Arrange - Get an exsisting flag from the hanky code.  
            var duplicateFlag = HankyCode.GetRandomFlag();
            var expectedCount = HankyCode.FlagCount;

            //Atempt to re-add it. Should fail and not increase count. 
            HankyCode.AddFlag(duplicateFlag);

            //Assert - that HankyCode's Flag count did not increase. 
            Assert.IsTrue(HankyCode.FlagCount == expectedCount);
        }

        [TestMethod]
        /// <summary>
        /// Gets a flag by its ID from the Hanky Code
        /// </summary>
		public void GetFlagByID()
		{
            //Arrange - get a random flag from the flag library
            var randFlag = HankyCode.GetRandomFlag();

            var retrivedFlag = HankyCode.GetFlagByID(randFlag.ID);

            //Assert
            Assert.AreEqual(randFlag, retrivedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Makes sure that the actual opposite flag is being obtained. 
        /// </summary>
		public void GetOppositeDonnedFlag()
        {
            //Arrange - get a random flag from the flag library and donn it. Then build its expected opposie. 
            var randFlag = HankyCode.GetRandomFlag();

            var donnedRandFlag = randFlag.DonnFlag(FlaggableLocations.Left);
            var expectedOppositeFlag = randFlag.DonnFlag(FlaggableLocations.Right);

            var oppositeFlag = HankyCode.GetOppositeDonnedFlag(donnedRandFlag);

            //Assert - expectedOppositeFlag's DonnedID equals oppositeFlag's DonnedID
            Assert.IsTrue(expectedOppositeFlag.DonnedID == oppositeFlag.DonnedID);
        }

        [TestMethod]
        /// <summary>
        /// Get a flag by its visual description. Important because
        /// crusing by a flag's description is how it happens in real life
        /// </summary>
        public void GetFlagByVisualDescription()
        {
            //Arrange - get the first key from the flag library
            var randFlag = HankyCode.GetRandomFlag();

            var retrivedFlag = HankyCode.GetFlagByVisualDescription(randFlag.VisualDescription);

            //Assert
            Assert.AreEqual(randFlag, retrivedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Get the corresponding donned flags for a donned flag. 
        /// </summary>
        public void GetCorrectCorrespondingDonnedFlags()
        {
            //Arrange - Create a hanky and donn it.
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);
            var expectedHanky = lightBlueHanky.DonnFlag(FlaggableLocations.Right);

            var correspondingHanky = HankyCode.GetCorrespondingDonnedFlags(lightBlueHankyWornOnLeft).FirstOrDefault();
            //var correspondingHanky = HankyCode.GetDonnedFlagByID(lightBlueHankyWornOnLeft.CorrespondingDonnedFlagID);

            //Assert - The corresponding hanky should ID should equal the expected hanky ID
            Assert.IsTrue(correspondingHanky.ID == expectedHanky.ID);
        }

        /// <summary>
        /// Create a hanky and get the same hanky from the HankCode. They
        /// should be equal when compared. 
        /// </summary>
        [TestMethod]
        public void FlagsAreEqualToEachotherWhenTheyHaveTheSameID()
        {
            var blackHanky = new CottonHanky(new SolidColor(ColorWheel.Black),
                    new AssociatedTrait("Heavy Sadomasochism", Rolls.TopBottomRolls));
            var alsoBlackHanky = HankyCode.GetFlagByID(blackHanky.ID);

            Assert.IsTrue(blackHanky == alsoBlackHanky);
            Assert.AreEqual(blackHanky, alsoBlackHanky);
        }

        /// <summary>
        /// Get the correct corresponding donned flags for the provided donned flags. 
        /// </summary>
        [TestMethod]
        public void GetCorrectCorrespondingDonnedFlagsForMultipleFlags()
        {
            //Arrange - get two random flags and don them left and right. The
            //expected results will be thouse same flags donned right and left
            var randFlag1 = HankyCode.GetRandomFlag();
            var randFlag2 = HankyCode.GetRandomFlag();

            var donnedFlags = new List<DonnedFlag>()
            {
                randFlag1.DonnFlag(FlaggableLocations.Left),
                randFlag2.DonnFlag(FlaggableLocations.Right)
            };
            
            var correspondingFlagsHash = HankyCode.GetCorrespondingDonnedFlags(donnedFlags).ToHashSet<DonnedFlag>();
            var expectedResults = new HashSet<DonnedFlag>()
            {
                randFlag1.DonnFlag(FlaggableLocations.Right),
                randFlag2.DonnFlag(FlaggableLocations.Left)
            };

            Console.Write("\n\ncorresponding:");
            PrintHashSet(correspondingFlagsHash);

            Console.Write("\n\nexpected:");
            PrintHashSet(expectedResults);

            //Assert - The expected results hash and the corresponding flags
            //hash should contain the same members
            Assert.IsTrue(correspondingFlagsHash.SetEquals(expectedResults));
        }

        private void PrintHashSet(HashSet<DonnedFlag> flagSet)
        {
            foreach (var flag in flagSet)
            {
                Console.Write("\n"+flag.VisualDescription + "-" + flag.DonnedID);
            }
        }

        /// <summary>
        /// Make sure donned flags is more than 1. 
        /// </summary>
        [TestMethod]
        public void GetDonnedLeftFlags()
        {
            Assert.IsTrue(HankyCode.GetAllDonnedFlags(FlaggableLocations.Left).Count > 0);
        }

        /// <summary>
        /// Make sure donned flags is more than 1. 
        /// </summary>
        [TestMethod]
        public void GetDonnedRightFlags()
        {
            Assert.IsTrue(HankyCode.GetAllDonnedFlags(FlaggableLocations.Right).Count > 0);
        }

        /// <summary>
        /// Compare the count of leftDonned Hankies and right to the count of
        /// get all to make sure they are the same. 
        /// </summary>
        [TestMethod]
        public void GetAllDonnedFlagsReturnsCorrect()
        {
            //Arrange - combine the lists and get the expected count.
            var expectedDonnedHash = HankyCode.GetAllDonnedFlags
                (FlaggableLocations.Left).Concat<DonnedFlag>
                (HankyCode.GetAllDonnedFlags(FlaggableLocations.Right))
                .ToHashSet<DonnedFlag>();
            var donnedHash = HankyCode.GetAllDonnedFlags().ToHashSet<DonnedFlag>();


            //Assert - compare the counts and make sure they have a value higher than 0.
            Assert.IsTrue(HankyCode.GetAllDonnedFlags().Count == expectedDonnedHash.Count);
            Assert.IsTrue(donnedHash.Count > 0);
            Assert.IsTrue(donnedHash.SetEquals(expectedDonnedHash));
        }

        [TestMethod]
        /// <summary>
        /// The HankyCode needs to be able to handle the special case of
        /// flagging orange left, wwitch should return all donned flags. 
        /// </summary>
        public void FlaggingOrangeLeftShowsAll()
        {
            //Arrange - create and donn an orange flag left. Get all donned 
            //flags from the Hanky code and put them in a list of expected
            //results. 
            var orangeHanky = HankyCode.GetFlagByVisualDescription
                (new SolidColor(ColorWheel.Orange).Description);
            var orangeLeft = orangeHanky.DonnFlag(FlaggableLocations.Left);

            var correspondingFlagsHash = HankyCode.GetCorrespondingDonnedFlags
                (orangeLeft).ToHashSet<DonnedFlag>();
            var expectedResultsHash = HankyCode.GetAllDonnedFlags()
                .ToHashSet<DonnedFlag>();


            //Assert - The expected results hash and the corresponding flags
            //hash should contain the same members
            Assert.IsTrue(correspondingFlagsHash.SetEquals(expectedResultsHash));
        }

    }
}

