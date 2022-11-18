using System;
using System.Security.Cryptography.X509Certificates;
using Hankies.Domain.HankyCode.Appearance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.Appearance
{
    [TestClass]
    public class ColorWheelTests
    {
        [TestMethod]
        public void HasCorrectColorNames()
        {
            Assert.AreEqual(ColorWheel.White.Name, "White");
            Assert.AreEqual(ColorWheel.Black.Name, "Black");
            Assert.AreEqual(ColorWheel.Grey.Name, "Grey");
            Assert.AreEqual(ColorWheel.Charcoal.Name, "Charcoal");
        }

        [TestMethod]
        public void HasCorrectColorHexs()
        {
            Assert.AreEqual(ColorWheel.White.Hex, "#ffffff");
            Assert.AreEqual(ColorWheel.Black.Hex, "#000000");
            Assert.AreEqual(ColorWheel.Grey.Hex, "#808080");
            Assert.AreEqual(ColorWheel.Charcoal.Hex, "#36454f");
        }
    }
}
