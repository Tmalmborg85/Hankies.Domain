using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class CorduroyTests
    {
        [TestMethod]
        public void KnownCorduroyFlagsAreCorrect()
        {
            //Arrange
            var brownCorduroy = new CorduroyHanky(ColorWheel.Brown, new AssociatedTrait("Proffessor", Rolls.IsIsIntoRolls));

            //Assert
            Assert.AreEqual("Brown corduroy", brownCorduroy.VisualDescription);
        }
    }
}
