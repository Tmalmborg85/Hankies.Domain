using System;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class CocktailNapkinTests
    {
        [TestMethod]
        public void KnownCocktailNapkinFlagsAreCorrect()
        {
            //Arrange
            var cocktailNapkin = new CocktailNapkin();

            //Assert
            Assert.AreEqual("Cocktail napkin", cocktailNapkin.Description);
        }
    }
}
