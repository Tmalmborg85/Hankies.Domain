using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class FlagsTests
    {
        [TestMethod]
        public void GenerateFlag()
        {
            //Arrange
            var flag = new CorduroyHanky(ColorWheel.Brown, new AssociatedTrait("Proffessor", Rolls.IsIsIntoRolls));

            Assert.IsTrue(flag != null);
        }

        [TestMethod]
        public void FlagHasID()
        {
            //Arrange - Create a hanky and donn it. 
            var lightBlueHanky = new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls));

            Console.Write("Flag ID: " + lightBlueHanky.ID.ToString());
            //Assert - Donned ID is not null or empty. 
            Assert.IsTrue(lightBlueHanky.ID != null);
            Assert.IsTrue(lightBlueHanky.ID != Guid.Empty);
        }

        [TestMethod]
        public void IDGenerationErrorsThrowFailedException()
        {
            Assert.ThrowsException<FailedToCreateFlagIDException>(new Action(()=>
                {
                    var flagWithBadIDseed = new ItemFlag(null, new AssociatedTrait("Nothing", Rolls.TopBottomRolls));
                }));

            Assert.ThrowsException<FailedToCreateFlagIDException>(new Action(() =>
            {
                var flagWithBadIDseed = new ItemFlag(string.Empty, new AssociatedTrait("Nothing", Rolls.TopBottomRolls));
            }));

            Assert.ThrowsException<FailedToCreateFlagIDException>(new Action(() =>
            {
                var flagWithBadIDseed = new ItemFlag(" ", new AssociatedTrait("Nothing", Rolls.TopBottomRolls));
            }));
        }

        /// <summary>
        /// Create a <c>DonnedFlag</c> object from a <c>Flag</c> object. 
        /// </summary>
        [TestMethod]
        public void CanDonnFlag()
        {
            //Arrange
            var lightBlueHanky = new DoffedFlag(new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls)));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);

            Assert.IsNotNull(lightBlueHankyWornOnLeft);
        }
    }
}
