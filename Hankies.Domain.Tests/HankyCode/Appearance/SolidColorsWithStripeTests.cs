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
            var vertStripe = new VerticalStripe(whiteColor);
            var expectedDescription = "Blue with vertical white stripe";

            var blueWithWhiteStripe = new SolidColorWithStripe(blueColor, vertStripe);

            //Assert
            Assert.AreEqual(expectedDescription, blueWithWhiteStripe.Description);

        }

        [TestMethod]
        public void SolidColorWStripeIDIsCorrect()
        {
            //Arrange
            var blueColor = new NamedColor("Blue", "#0000CD");
            var whiteColor = new NamedColor("White", "#ffffff");
            var vertStripe = new VerticalStripe(whiteColor);
            var expectedID = "BLUEWITHVERTICALWHITESTRIPE";

            var blueWithWhiteStripe = new SolidColorWithStripe(blueColor, vertStripe);

            //Assert
            Assert.AreEqual(expectedID, blueWithWhiteStripe.ID);

        }
    }
}
