﻿using System;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Flag;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Flag
{
    [TestClass]
    public class FurTests
    {
        [TestMethod]
        public void FurDescriptionIsCorrect()
        {
            //arrange
            var fur = new Fur();

            //Assert
            Assert.AreEqual("Fur", fur.Description);
        }
    }
}
