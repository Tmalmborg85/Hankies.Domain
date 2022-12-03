using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class ItemHankyTests
    {
        [TestMethod]
        public void FurDescriptionIsCorrect()
        {
            //arrange
            var fur = new ItemFlag("Fur", new AssociatedTrait("Bestiality", Rolls.TopBottomRolls));

            //Assert
            Assert.AreEqual("Fur", fur.VisualDescription);
        }

        [TestMethod]
        public void CoctailNapkinDescriptionIsCorrect()
        {
            //arrange
            var cocktailNapkin = new ItemFlag("Cocktail napkin", new AssociatedTrait("Bartender", Rolls.IsIsIntoRolls));

            //Assert
            Assert.AreEqual("Cocktail napkin", cocktailNapkin.VisualDescription);
        }

        [TestMethod]
        public void DirtyJockstrapDescriptionIsCorrect()
        {
            //arrange
            var dirtyJockstrap = new ItemFlag("Dirty jockstrap", new AssociatedTrait("Dirty Jockstraps", Rolls.CustomRolls("Wears one", "Wants to suck one clean")));

            //Assert
            Assert.AreEqual("Dirty jockstrap", dirtyJockstrap.VisualDescription);
        }

        [TestMethod]
        public void DoilyDescriptionIsCorrect()
        {
            //arrange
            var doily = new ItemFlag("Doily", new AssociatedTrait("Tea Room", Rolls.CustomRolls("Pours", "Drinks")));

            //Assert
            Assert.AreEqual("Doily", doily.VisualDescription);
        }

        [TestMethod]
        public void MosquitoNettingDescriptionIsCorrect()
        {
            //arrange
            var mosquitoNetting = new ItemFlag("Mosquito netting", new AssociatedTrait("Outdoor Sex", Rolls.TopBottomRolls));

            //Assert
            Assert.AreEqual("Mosquito netting", mosquitoNetting.VisualDescription);
        }

        [TestMethod]
        public void TeddyBearDescriptionIsCorrect()
        {
            //arrange
            var teddyBear = new ItemFlag("Teaddy bear", new AssociatedTrait("Cuddling", Rolls.CustomRolls("Cuddler", "Cuddlee")));

            //Assert
            Assert.AreEqual("Teaddy bear", teddyBear.VisualDescription);
        }

        [TestMethod]
        public void TerryClothDescriptionIsCorrect()
        {
            //arrange
            var terryCloth = new ItemFlag("Terry cloth", new AssociatedTrait("Bathouse", Rolls.TopBottomRolls));

            //Assert
            Assert.AreEqual("Terry cloth", terryCloth.VisualDescription);
        }

        [TestMethod]
        public void TissieDescriptionIsCorrect()
        {
            //arrange
            var tissue = new ItemFlag("Tissue", new AssociatedTrait("Smell", Rolls.CustomRolls("Stinks", "Sniffs")));

            //Assert
            Assert.AreEqual("Tissue", tissue.VisualDescription);
        }

        [TestMethod]
        public void CarKeysDescriptionIsCorrect()
        {
            //arrange
            var carKeys = new ItemFlag("Car keys", new AssociatedTrait("Car", Rolls.CustomRolls("Has a car", "Needs a ride")));

            //Assert
            Assert.AreEqual("Car keys", carKeys.VisualDescription);
        }

        [TestMethod]
        public void HouseKeysDescriptionIsCorrect()
        {
            //arrange
            var houseKeys = new ItemFlag("House keys", new AssociatedTrait("House", Rolls.CustomRolls("Has a home", "Needs a place to stay")));

            //Assert
            Assert.AreEqual("House keys", houseKeys.VisualDescription);
        }
    }
}
