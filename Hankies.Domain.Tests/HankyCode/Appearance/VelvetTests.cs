using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Appearance.Fabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
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

            var whiteVelvet = new Velvet(whiteColor);

            //Assert
            Assert.AreEqual(expectedDescription, whiteVelvet.Description);

        }

        [TestMethod]
        public void VelvetIDIsCorrect()
        {
            //Arrange
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedID = "WHITEVELVET";

            var whiteVelvet = new Velvet(whiteColor);

            //Assert
            Assert.AreEqual(expectedID, whiteVelvet.ID);
        }
    }
}
