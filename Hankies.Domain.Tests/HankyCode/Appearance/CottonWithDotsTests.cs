using System;
using Hankies.Domain.HankyCode.Appearance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class CottonWithDotsTests
    {
        [TestMethod]
        public void CottonWDotsDescriptionIsCorrect()
        {
            //Arrange
            var blueColor = new NamedColor("Blue", "#0000CD");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedDescription = "Blue with white dots";

            var blueWithWhiteDots = new ColorWithDots(blueColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedDescription, blueWithWhiteDots.Description);

        }

        [TestMethod]
        public void CottonWDotsIDIsCorrect()
        {
            //Arrange
            var blueColor = new NamedColor("Blue", "#0000CD");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedID = "BLUEWITHWHITEDOTS";

            var blueWithWhiteDots = new ColorWithDots(blueColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedID, blueWithWhiteDots.ID);

        }
    }
}
