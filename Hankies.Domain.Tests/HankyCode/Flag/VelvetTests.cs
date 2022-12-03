using System;
using System.Drawing;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class VelvetTests
    {
        [TestMethod]
        public void KnownVelvetFlagsVisualDescriptionsAreCorrect()
        {
            //Arrange
            var blackVelvet = new VelvetHanky(ColorWheel.Black,
                    new AssociatedTrait("Video", Rolls.CustomRolls("Likes to take video", "Likes to be on camera")));
            var whiteVelvet = new VelvetHanky(ColorWheel.White,
                new AssociatedTrait("Voyeur", Rolls.CustomRolls("Watcher", "Like to be watched")));

            //Assert
            Assert.AreEqual("Black velvet", blackVelvet.VisualDescription);
            Assert.AreEqual("White velvet", whiteVelvet.VisualDescription);
        }
    }
}
