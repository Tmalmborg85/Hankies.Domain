using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hankies.Domain.HankyCode;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;
using Hankies.Domain.HankyCode.Flag;
using Hankies.Domain.HankyCode.Interpritation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hankies.Domain.Tests.HankyCode.HankyCodeServices
{
	/// <summary>
	/// Use these tests to make sure hanky code translation works.
	/// <remarks>
	/// Using TTD (Test Driven Design) so theses tests will be written first.
	/// Start writing here and then create classes, and thier tests, as needed.
	/// By the end there should be a fully functioning Hanky Code Service. 
	/// </remarks>
	/// </summary>
	[TestClass]
	public class HankyCodeServiceTests
	{
		public HankyCodeService HankyCode { get; set; }

		public HankyCodeServiceTests()
		{
            HankyCode = HankyCodeTestingToolkit.PrePopulatedHankyCodeService();
        }

        /// <summary>
        /// Add a new undonned(not worn) flag to the HankyCode.  
        /// </summary>
        //      [TestMethod]
        //      public void AddNewFlagToHankyCode()
        //{
        //          //Arrange
        //          //create a new flag with appearance
        //          var newFlag = new DoffedFlag(new CottonHanky(
        //              new SolidColor(ColorWheel.RobinEggBlue),
        //              new AssociatedTrait("69", Rolls.IntoNotIntoRolls, "Mutual oral sex")));

        //          //add the flag to the hanky code.
        //          HankyCode.AddFlag(newFlag);

        //	//Assert
        //	Assert.IsTrue(HankyCode.HasFlag(newFlag));
        //      }

        /// <summary>
        /// Should fail to add a duplicate flag to the hanky code.   
        /// </summary>
        //[TestMethod]
        //public void FailToAddDuplicateFlagToHankyCode()
        //{
        //    //Arrange - Get an exsisting flag from the hanky code.  
        //    var duplicateFlag = HankyCode.GetRandomDoffedFlag();
        //    var expectedCount = HankyCode.FlagCount;

        //    //Atempt to re-add it. Should fail and not increase count. 
        //    HankyCode.AddFlag(duplicateFlag);

        //    //Assert - that HankyCode's Flag count did not increase. 
        //    Assert.IsTrue(HankyCode.FlagCount == expectedCount);
        //}

        /// <summary>
        /// Save the HankyCode to make sure there is a file to load.
        /// Load the json file. Then Serialize it into a model. The model should have the same 
        /// </summary>
        [TestMethod]
        public void LoadHankyCodeModelFromJSONFile()
        {
            //HankyCode.SaveHankyCode("HankyCode_TESTING.JSON");

            //var hankyCodeModel = HankyCode.LoadHankyCodeModel("HankyCode_TESTING.JSON");
            Assert.Fail();
        }

        /// <summary>
        /// Build a HankyCodeModel from the service, increase the version to
        /// similate the save, then convert to JSON. Save the hanky code and
        /// make sure the JSONS are the same. 
        /// </summary>
        [TestMethod]
        public void SaveHankyCodeToJSONFile()
        {
            var hankyModel = HankyCode.ToHankyCodeModel();
            hankyModel.Version++;
            var expectedHankyCodeJSON = hankyModel.ToJSON();

            HankyCode.SaveHankyCode("HankyCode_TESTING.JSON");

            var hankyCodeJSON = HankyCode.ToHankyCodeModel().ToJSON();

            Assert.IsTrue(hankyCodeJSON == expectedHankyCodeJSON);
        }

        [TestMethod]
        public void RecomendNewFlagToHankyCode()
        {
            //Arrange -create a new doffed flag and recomend it to the code.
            var newFlag = new RecomendedFlag(new CottonHanky(
                new SolidColor(ColorWheel.RobinEggBlue),
                new AssociatedTrait("69", Rolls.IntoNotIntoRolls, "Mutual oral sex")));

            HankyCode.RecomendNewFlagToHankyCode(newFlag);
            var recomededFlags = HankyCode.GetRecomendedFlags().ToHashSet<RecomendedFlag>();

            //Assert - recomended flag must contain the new flag
            Assert.IsTrue(recomededFlags.Contains(newFlag));
        }

        /// <summary>
        /// create a new recomended flag and recomend it to the code. Get the
        /// count of recomended flags, that is the expected result. Then
        /// recomend the flag again. the count of recomended flags should not increase
        /// </summary>
        [TestMethod]
        public void CantRecomendDuplicateOrExsistingFlagToHankyCode()
        {
            var newFlag = new RecomendedFlag(new CottonHanky(
                new SolidColor(ColorWheel.RobinEggBlue),
                new AssociatedTrait("69", Rolls.IntoNotIntoRolls, "Mutual oral sex")));
            var duplicateNewFlag = newFlag;
            var exsistingFlag = new RecomendedFlag(HankyCode.GetRandomDoffedFlag());

            HankyCode.RecomendNewFlagToHankyCode(newFlag);
            var expectedCount = HankyCode.GetRecomendedFlags().Count;
            HankyCode.RecomendNewFlagToHankyCode(duplicateNewFlag);
            HankyCode.RecomendNewFlagToHankyCode(exsistingFlag);
            var actualCount = HankyCode.GetRecomendedFlags().Count;

            Assert.IsTrue(actualCount == expectedCount);
        }

        [TestMethod]
        public void ApprovingRecomendedFlagAddsFlagToDoffed()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ApprovingMissingRecomendedFlagDoesNothing()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ApprovingRecomendedFlagRemovesFlagFromRecomended()
        {
            Assert.Fail();
        }

        [TestMethod]
        /// <summary>
        /// Gets a flag by its ID from the Hanky Code
        /// </summary>
		public void GetDoffedFlagByID()
		{
            //Arrange - get a random flag from the flag library
            var randFlag = HankyCode.GetRandomDoffedFlag();

            var retrivedFlag = HankyCode.GetFlagByID(randFlag.ID);

            //Assert
            Assert.AreEqual(randFlag, retrivedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Gets a donned flag by its DoffedID from the Hanky Code
        /// </summary>
		public void GetDonnedFlagByDoffedID()
        {
            //Arrange - get a random flag from the flag library, then donn it
            var randFlag = HankyCode.GetRandomDoffedFlag();
            var donnedLeftRandFlag = randFlag.DonnFlag(FlaggableLocations.Left);

            var retrivedDonnedFlag = HankyCode.GetDonnedFlagByDoffedID(donnedLeftRandFlag.DoffedID, FlaggableLocations.Left);

            //Assert
            Assert.AreEqual(donnedLeftRandFlag, retrivedDonnedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Gets a donned flag by its DonnedID from the Hanky Code
        /// </summary>
		public void GetDonnedFlagByDonnedID()
        {
            //Arrange - get a random flag from the flag library, then donn it
            var randFlag = HankyCode.GetRandomDoffedFlag();
            var donnedRightRandFlag = randFlag.DonnFlag(FlaggableLocations.Right);

            var retrivedDonnedFlag = HankyCode.GetDonnedFlagByID(donnedRightRandFlag.ID);

            //Assert
            Assert.AreEqual(donnedRightRandFlag, retrivedDonnedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Makes sure that the actual opposite flag is being obtained. 
        /// </summary>
		public void GetOppositeDonnedFlag()
        {
            //Arrange - get a random flag from the flag library and donn it. Then build its expected opposie. 
            var randFlag = HankyCode.GetRandomDoffedFlag();

            var donnedRandFlag = randFlag.DonnFlag(FlaggableLocations.Left);
            var expectedOppositeFlag = randFlag.DonnFlag(FlaggableLocations.Right);

            var oppositeFlag = HankyCode.GetOppositeDonnedFlag(donnedRandFlag);

            //Assert - expectedOppositeFlag's ID equals oppositeFlag ID
            Assert.IsTrue(expectedOppositeFlag == oppositeFlag);
        }

        [TestMethod]
        /// <summary>
        /// Makes sure that a non-exsistant Donned ID returns null
        /// </summary>
		public void GetOppositeFlagReturnsNullIfNoOpositeFlagExsists()
        {
            //Arrange - create a flag not in the code. Then donn it and try to get its opposite. 
            var testFlag = new DoffedFlag(new VelvetHanky(ColorWheel.Lavender,
                new AssociatedTrait("Testies!", Rolls.CustomRolls("test", "testerson"))));

            var donnedTestFlag = testFlag.DonnFlag(FlaggableLocations.Left);

            var oppositeFlag = HankyCode.GetOppositeDonnedFlag(donnedTestFlag);

            //Assert - oposite flag should be null because thee is no oposite for test flag
            Assert.IsNull(oppositeFlag);
        }

        [TestMethod]
        /// <summary>
        /// Get a flag by its visual description. Important because
        /// crusing by a flag's description is how it happens in real life
        /// </summary>
        public void GetFlagByVisualDescription()
        {
            //Arrange - get the first key from the flag library
            var randFlag = HankyCode.GetRandomDoffedFlag();

            var retrivedFlag = HankyCode.GetFlagByVisualDescription(randFlag.VisualDescription);

            //Assert
            Assert.AreEqual(randFlag, retrivedFlag);
        }

        [TestMethod]
        /// <summary>
        /// Get the corresponding donned flags for a donned flag. 
        /// </summary>
        public void GetCorrectCorrespondingDonnedFlags()
        {
            //Arrange - Create a hanky and donn it.
            var lightBlueHanky = new DoffedFlag(new CottonHanky(new SolidColor(ColorWheel.LightBlue),
                    new AssociatedTrait("Oral sex", Rolls.TopBottomRolls)));

            var lightBlueHankyWornOnLeft = lightBlueHanky.DonnFlag(FlaggableLocations.Left);
            var expectedHanky = lightBlueHanky.DonnFlag(FlaggableLocations.Right);

            var correspondingHanky = HankyCode.GetCorrespondingDonnedFlags(lightBlueHankyWornOnLeft).FirstOrDefault();
            //var correspondingHanky = HankyCode.GetDonnedFlagByID(lightBlueHankyWornOnLeft.CorrespondingDonnedFlagID);

            //Assert - The corresponding hanky should ID should equal the expected hanky ID
            Assert.IsTrue(correspondingHanky.ID == expectedHanky.ID);
        }

        /// <summary>
        /// Create a hanky and get the same hanky from the HankCode. They
        /// should be equal when compared. 
        /// </summary>
        [TestMethod]
        public void FlagsAreEqualToEachotherWhenTheyHaveTheSameID()
        {
            var blackHanky = new CottonHanky(new SolidColor(ColorWheel.Black),
                    new AssociatedTrait("Heavy Sadomasochism", Rolls.TopBottomRolls));
            var alsoBlackHanky = HankyCode.GetFlagByID(blackHanky.ID);

            Assert.IsTrue(blackHanky == alsoBlackHanky);
            Assert.AreEqual(blackHanky, alsoBlackHanky);
        }

        /// <summary>
        /// Create a hanky, donn it, and get the same hanky from the HankCode.
        /// They should be equal when compared. 
        /// </summary>
        [TestMethod]
        public void DonnedFlagsAreEqualToEachotherWhenTheyHaveTheSameID()
        {
            var donnedBlackHanky = new DoffedFlag(new CottonHanky(
                new SolidColor(ColorWheel.Black),
                new AssociatedTrait("Heavy Sadomasochism",
                Rolls.TopBottomRolls))).DonnFlag(FlaggableLocations.Left);

            var sameDonnedBlackHanky = HankyCode.GetDonnedFlagByDoffedID(donnedBlackHanky.DoffedID, FlaggableLocations.Left);

            Assert.IsTrue(donnedBlackHanky == sameDonnedBlackHanky);
            Assert.AreEqual(donnedBlackHanky, sameDonnedBlackHanky);
        }

        /// <summary>
        /// Get the correct corresponding donned flags for the provided donned flags. 
        /// </summary>
        [TestMethod]
        public void GetCorrectCorrespondingDonnedFlagsForMultipleFlags()
        {
            //Arrange - get two random flags and don them left and right. The
            //expected results will be thouse same flags donned right and left
            var randFlag1 = HankyCode.GetRandomDoffedFlag();
            var randFlag2 = HankyCode.GetRandomDoffedFlag();

            var donnedFlags = new List<DonnedFlag>()
            {
                randFlag1.DonnFlag(FlaggableLocations.Left),
                randFlag2.DonnFlag(FlaggableLocations.Right)
            };
            
            var correspondingFlagsHash = HankyCode.GetCorrespondingDonnedFlags(donnedFlags).ToHashSet<DonnedFlag>();
            var expectedResults = new HashSet<DonnedFlag>()
            {
                randFlag1.DonnFlag(FlaggableLocations.Right),
                randFlag2.DonnFlag(FlaggableLocations.Left)
            };

            Console.Write("\n\ncorresponding:");
            PrintHashSet(correspondingFlagsHash);

            Console.Write("\n\nexpected:");
            PrintHashSet(expectedResults);



            //Assert - The expected results hash and the corresponding flags
            //hash should contain the same members
            Assert.IsTrue(correspondingFlagsHash.SetEquals(expectedResults));
            //Assert.IsTrue(correspondingFlagsHash == expectedResults);
        }

        private void PrintHashSet(HashSet<DonnedFlag> flagSet)
        {
            foreach (var flag in flagSet)
            {
                Console.Write("\n"+flag.VisualDescription + "-" + flag.ID);
            }
        }

        /// <summary>
        /// Make sure donned flags is more than 1. 
        /// </summary>
        [TestMethod]
        public void GetDonnedLeftFlags()
        {
            Assert.IsTrue(HankyCode.GetAllDonnedFlags(FlaggableLocations.Left).Count > 0);
        }

        /// <summary>
        /// Make sure donned flags is more than 1. 
        /// </summary>
        [TestMethod]
        public void GetDonnedRightFlags()
        {
            Assert.IsTrue(HankyCode.GetAllDonnedFlags(FlaggableLocations.Right).Count > 0);
        }

        /// <summary>
        /// Compare the count of leftDonned Hankies and right to the count of
        /// get all to make sure they are the same. 
        /// </summary>
        [TestMethod]
        public void GetAllDonnedFlagsReturnsCorrect()
        {
            //Arrange - combine the lists and get the expected count.
            var expectedDonnedHash = HankyCode.GetAllDonnedFlags
                (FlaggableLocations.Left).Concat<DonnedFlag>
                (HankyCode.GetAllDonnedFlags(FlaggableLocations.Right))
                .ToHashSet<DonnedFlag>();
            var donnedHash = HankyCode.GetAllDonnedFlags().ToHashSet<DonnedFlag>();


            //Assert - compare the counts and make sure they have a value higher than 0.
            Assert.IsTrue(HankyCode.GetAllDonnedFlags().Count == expectedDonnedHash.Count);
            Assert.IsTrue(donnedHash.Count > 0);
            Assert.IsTrue(donnedHash.SetEquals(expectedDonnedHash));
        }

        [TestMethod]
        /// <summary>
        /// The HankyCode needs to be able to handle the special case of
        /// flagging orange left, wwitch should return all donned flags. 
        /// </summary>
        public void FlaggingOrangeLeftShowsAll()
        {
            //Arrange - create and donn an orange flag left. Get all donned 
            //flags from the Hanky code and put them in a list of expected
            //results. 
            var orangeHanky = HankyCode.GetFlagByVisualDescription
                (new SolidColor(ColorWheel.Orange).Description);
            var orangeLeft = orangeHanky.DonnFlag(FlaggableLocations.Left);

            var correspondingFlagsHash = HankyCode.GetCorrespondingDonnedFlags
                (orangeLeft).ToHashSet<DonnedFlag>();
            var expectedResultsHash = HankyCode.GetAllDonnedFlags()
                .ToHashSet<DonnedFlag>();


            //Assert - The expected results hash and the corresponding flags
            //hash should contain the same members
            Assert.IsTrue(correspondingFlagsHash.SetEquals(expectedResultsHash));
        }

    }
}

