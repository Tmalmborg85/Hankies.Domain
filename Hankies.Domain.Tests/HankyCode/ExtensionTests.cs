using System;
using Hankies.Domain.HankyCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode
{
    [TestClass]
    public class ExtensionTests
	{
		public ExtensionTests()
		{
		}

		[TestMethod]
		public void IsTypeComparisonReturnsTrueWhenTypesAreSame()
		{
			int objectA = 52;

			Assert.IsTrue(objectA.IsType<int>());
		}

        [TestMethod]
        public void IsTypeComparisonReturnsFalseWhenTypesAreNotSame()
        {
            int objectA = 52;

            Assert.IsFalse(objectA.IsType<string>());
        }

        [TestMethod]
        public void IsSameTypeAsComparisonReturnsTrueWhenTypesAreSame()
        {
            int objectA = 52;
            int objectB = 38;

            Assert.IsTrue(objectA.IsSameTypeAs(objectB));
        }

        [TestMethod]
        public void IsSameTypeAsComparisonReturnsFalseWhenTypesNotAreSame()
        {
            int objectA = 52;
            string objectB = "38";

            Assert.IsFalse(objectA.IsSameTypeAs(objectB));
        }

        [TestMethod]
        public void IsJSONReturnsTrueOnValidJSONStrings()
        {
            var validJSONString = "{\"Date\":\"2019-08-01T00:00:00-07:00\",\"TemperatureCelsius\":25,\"Summary\":\"Hot\"}";
            Assert.IsTrue(validJSONString.IsJson());
        }

        [TestMethod]
        public void IsJSONReturnsFalseOnInValidJSONStrings()
        {
            var validJSONString = "I am not JSON";
            Assert.IsFalse(validJSONString.IsJson());
        }
    }
}

