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
            Assert.AreEqual(ColorWheel.LightBlue.Name, "Light Blue");
            Assert.AreEqual(ColorWheel.Brown.Name, "Brown");
            Assert.AreEqual(ColorWheel.Yellow.Name, "Yellow");
        }

        [TestMethod]
        public void HasCorrectColorHexs()
        {
            Assert.AreEqual(ColorWheel.White.Hex, "#ffffff");
            Assert.AreEqual(ColorWheel.Black.Hex, "#000000");
            Assert.AreEqual(ColorWheel.Grey.Hex, "#808080");
            Assert.AreEqual(ColorWheel.Charcoal.Hex, "#36454f");
            Assert.AreEqual(ColorWheel.LightBlue.Hex, "#add8e6");
            Assert.AreEqual(ColorWheel.Brown.Hex, "#964b00");
            Assert.AreEqual(ColorWheel.Yellow.Hex, "#ffff00");
        }
    }
}
