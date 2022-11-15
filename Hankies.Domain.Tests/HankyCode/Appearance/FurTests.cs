using System;
using Hankies.Domain.HankyCode.Appearance.Fabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class FurTests
    {
        [TestMethod]
        public void FurDescriptionIsCorrect()
        {
            //arrange
            var fur = new Fur();
            var expectedDescription = "Fur";

            //Assert
            Assert.AreEqual(expectedDescription, fur.Description);
        }

        [TestMethod]
        public void FurIDIsCorrect()
        {
            //arrange
            var fur = new Fur();
            var expectedID = "FUR";

            //Assert
            Assert.AreEqual(expectedID, fur.ID);
        }
    }
}
