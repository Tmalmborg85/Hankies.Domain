using System;
using Hankies.Domain.HankyCode.Fetish;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Trait
{
	[TestClass]
	public class AssociatedTraitTests
	{
		[TestMethod]
		public void CreateTraitWithNameAndRolls()
		{
            //Arrange
            var trait = new AssociatedTrait("Sadomasochism",
                Rolls.TopBottomRolls);

            //Assert
            Assert.AreEqual("Sadomasochism", trait.Name);
        }

        [TestMethod]
        public void CreateTraitWithNameRollsAndDescription()
        {
            //Arrange
            var trait = new AssociatedTrait("Sadomasochism",
                Rolls.TopBottomRolls,
                "the giving and receiving of pleasure from acts involving the receipt or infliction of pain or humiliation.");

            //Assert
            Assert.AreEqual("the giving and receiving of pleasure from acts involving the receipt or infliction of pain or humiliation.", trait.Definition);
        }

        [TestMethod]
        public void CreateTraitWithNameRollsDescriptionAndHistory()
        {
            //Arrange
            var trait = new AssociatedTrait("Sadomasochism",
                Rolls.TopBottomRolls,
                "the giving and receiving of pleasure from acts involving the receipt or infliction of pain or humiliation.",
                "The German psychiatrist Richard von Krafft-Ebing introduced the terms \"Sadism\" and \"Masochism\"' into medical terminology in his work Neue Forschungen auf dem Gebiet der Psychopathia sexualis in 1890");

            //Assert
            Assert.AreEqual("The German psychiatrist Richard von Krafft-Ebing introduced the terms \"Sadism\" and \"Masochism\"' into medical terminology in his work Neue Forschungen auf dem Gebiet der Psychopathia sexualis in 1890", trait.History);
        }
    }
}

