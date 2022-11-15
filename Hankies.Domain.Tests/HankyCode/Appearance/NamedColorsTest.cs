using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hankies.Domain.HankyCode.Appearance;
namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class NamedColorsTest
    {
        [TestMethod]
        public void ColorHexValueIsValid()
        {
            //Arrange
            var color = new NamedColor("White", "#ffffff");

            //Assert
            Assert.AreEqual("#ffffff", color.Hex);
        }

        [TestMethod]
        public void ColorWontTakeInvalidHex()
        {
            //Arrange
            var color = new NamedColor("White", "#ffffff000000");

            //Assert
            Assert.IsNull(color.Hex);
        }

        [TestMethod]
        public void SingleColorsPrintASingleColorDescription()
        {
            //Arrange
            var appearance = new SolidColor(new NamedColor("White", "#ffffff"));
            var expectedDescription = "White";

            //Assert
            Assert.AreEqual(expectedDescription, appearance.Description);
        }

        [TestMethod]
        public void SingleColorsIDisCorrect()
        {
            //Arrange
            var appearance = new SolidColor(new NamedColor("White", "#ffffff"));
            var expectedID = "WHITE";

            //Assert
            Assert.AreEqual(expectedID, appearance.ID);
        }
    }
}
