using System;
using Hankies.Domain.HankyCode.Appearance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class SolidColorsWithStripeTests
    {
        [TestMethod]
        public void SolidColorWStripeDescriptionIsCorrect()
        {
            //Arrange
            var blueColor = new NamedColor("Blue", "#0000CD");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedDescription = "Blue with white stripe";

            var blueWithWhiteStripe = new ColorWithStripe(blueColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedDescription, blueWithWhiteStripe.Description);

        }

        [TestMethod]
        public void SolidColorWStripeIDIsCorrect()
        {
            //Arrange
            var blueColor = new NamedColor("Blue", "#0000CD");
            var whiteColor = new NamedColor("White", "#ffffff");
            var expectedID = "BLUEWITHWHITESTRIPE";

            var blueWithWhiteStripe = new ColorWithStripe(blueColor, whiteColor);

            //Assert
            Assert.AreEqual(expectedID, blueWithWhiteStripe.ID);

        }
    }
}
