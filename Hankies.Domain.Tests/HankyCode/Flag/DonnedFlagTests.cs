using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
	[TestClass]
	public class DonnedFlagTests
	{
		public DonnedFlagTests()
		{
		}

		[TestMethod]
        public void DonnFlagHasBaseFlagID()
        {
            //Arrange - Create a hanky and donn it. 
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);

            //Assert - The IDs are the same
            Assert.IsTrue(lightBlueHankyWornOnLeft.ID == lightBlueHanky.ID);
        }

        [TestMethod]
        public void DonnedFlagHasDonnedID()
        {
            //Arrange - Create a hanky and donn it. 
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);

            Console.Write("DonnedID: " + lightBlueHankyWornOnLeft.DonnedID.ToString());

            //Assert - Donned ID is not null or empty. 
            Assert.IsTrue(lightBlueHankyWornOnLeft.DonnedID != null);
            Assert.IsTrue(lightBlueHankyWornOnLeft.DonnedID != Guid.Empty);
        }

        [TestMethod]
        public void DonnedFlagHasCorrectCorrespondingDonnedFlag()
        {
            //Arrange - Create a hanky and donn it. 
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);
            var expectedCorrespondingHanky = lightBlueHanky.DonnFlag(FlaggableLocations.Right);

            Console.Write("Actual Corresponding DonnedID: " + lightBlueHankyWornOnLeft.CorrespondingDonnedFlagID.ToString());
            Console.Write("\nExpected Corresponding DonnedID: " + expectedCorrespondingHanky.DonnedID.ToString());

            //Assert - The the left hanky's CorrespondingDonnedFlag should be the same as the right 
            Assert.IsTrue(lightBlueHankyWornOnLeft.CorrespondingDonnedFlagID == expectedCorrespondingHanky.DonnedID);
            Assert.IsTrue(expectedCorrespondingHanky.CorrespondingDonnedFlagID == lightBlueHankyWornOnLeft.DonnedID);
        }

        [TestMethod]
        public void CorrectlyBuildsVisualDescription()
        {
            //Arrange - Create a hanky and donn it. 
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);

            var expectedVisualDescription = "Light blue worn on the left";

            Console.Write(lightBlueHankyWornOnLeft.VisualDescription);
            //Assert - That the donned flag description matches the expeced description
            Assert.IsTrue(lightBlueHankyWornOnLeft.VisualDescription == expectedVisualDescription);
            
        }
    }
}

