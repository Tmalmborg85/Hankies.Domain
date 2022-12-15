using System;
using System.Threading;
using Hankies.Domain.HankyCode;
using Hankies.Domain.HankyCode.Interpritation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.HankyCodeServices
{
	[TestClass]
	public class HankyCodeModelTests
	{
		public HankyCodeModelTests()
		{
		}

		/// <summary>
		/// Simply build an empty model and assert it is not null.
		/// </summary>
		[TestMethod]
        public void CanBuildEmptyModelWithoutErrors()
        {
			var emptyModel = new HankyCodeModel();
			Assert.IsNotNull(emptyModel);
        }

		/// <summary>
		/// Get a prepopulated HankyCodeService, then build a model from it.
		/// Assert that the model is not null and has a flag count. 
		/// </summary>
		[TestMethod]
        public void CanBuildModelFromHankyCode()
		{
            var hankyCode = HankyCodeTestingToolkit.PrePopulatedHankyCodeService();
			var hankyCodeModel = hankyCode.ToHankyCodeModel();

			Assert.IsNotNull(hankyCodeModel);
			Assert.IsTrue(hankyCodeModel.Flags.Count > 0);
        }

        /// <summary>
        /// Get a prepopulated HankyCodeService, then build a model from it.
		/// Convert the model using ToJSON(). Validate the JSON is a valid
        /// </summary>
        public void ToJSONProducesValidJSONInPopulatedModel()
		{
            var hankyCode = HankyCodeTestingToolkit.PrePopulatedHankyCodeService();
            var hankyCodeModel = hankyCode.ToHankyCodeModel();
			var hankyCodeJSON = hankyCodeModel.ToJSON();

			Assert.IsTrue(hankyCodeJSON.IsJson());
        }

        /// <summary>
        /// Build an empty HankyCodeService, then build a model from it.
		/// Convert the model using ToJSON(). Validate the JSON is a valid
        /// </summary>
        public void ToJSONProducesValidEmptyJSONInUnPopulatedModel()
        {
			var hankyCode = new HankyCodeService();
            var hankyCodeModel = hankyCode.ToHankyCodeModel();
            var hankyCodeJSON = hankyCodeModel.ToJSON();

            Assert.IsTrue(hankyCodeJSON.IsJson());
        }
    }
}

