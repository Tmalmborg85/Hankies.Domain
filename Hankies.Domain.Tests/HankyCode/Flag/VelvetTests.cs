using System;
using System.Drawing;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class VelvetTests
    {
        [TestMethod]
        public void VelvetDescriptionIsCorrect()
        {
            //Arrange
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedDescription = "White velvet";

            var whiteVelvet = new VelvetHanky(whiteColor);

            //Assert
            Assert.AreEqual(expectedDescription, whiteVelvet.Description);

        }

        [TestMethod]
        public void KnownVelvetFlagsAreCorrect()
        {
           // var blackVelvet = new VelvetHanky()
        }
    }
}
