using System;
using Hankies.Domain.HankyCode.Appearance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class SolidColorWithCheckTests
    {
        [TestMethod]
        public void SolidColorWCheckDescriptionIsCorrect()
        {
            //Arrange
            var blackColor = new NamedColor("Black", "#000000");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedDescription = "Black with white check";

            var blackWithWhiteCheck = new Checkerboard(blackColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedDescription, blackWithWhiteCheck.Description);

        }

        [TestMethod]
        public void SolidColorWCheckIDIsCorrect()
        {
            //Arrange
            var blackColor = new NamedColor("Black", "#000000");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedID = "BLACKWITHWHITECHECK";

            var blackWithWhiteCheck = new Checkerboard(blackColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedID, blackWithWhiteCheck.ID);

        }
    }
}
