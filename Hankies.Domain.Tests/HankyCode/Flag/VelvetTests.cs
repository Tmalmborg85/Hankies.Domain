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
        public void KnownVelvetFlagsAreCorrect()
        {
            //Arrange
            var blackVelvet = new VelvetHanky(ColorWheel.Black);
            var whiteVelvet = new VelvetHanky(ColorWheel.White);

            //Assert
            Assert.AreEqual("Black velvet", blackVelvet.Description);
            Assert.AreEqual("White velvet", whiteVelvet.Description);
        }
    }
}
