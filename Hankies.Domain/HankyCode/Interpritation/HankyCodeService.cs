using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using Hankies.Domain.HankyCode.Flag;

namespace Hankies.Domain.HankyCode.Interpritation
{
	public class HankyCodeService
	{
        private Dictionary<Guid, RecomendedFlag> RecomendedFlags { get; set; }
        private Dictionary<Guid, DoffedFlag> DoffedFlags { get; set; }
        private Dictionary<Guid, DonnedFlag> DonnedFlags { get; set; }
        private Dictionary<Guid, DonnedFlag> DonnedLeftFlags { get; set; }
        private Dictionary<Guid, DonnedFlag> DonnedRightFlags { get; set; }
        private Dictionary<string, Guid> FlagIDsByDescription { get; set; }
        private List<Guid> FlagKeys { get; set; }

        public Decimal HankyCodeVersion { get; private set; }

        /// <summary>
        /// The total number of flags currently in this Hanky Code. 
        /// </summary>
        public int FlagCount => DoffedFlags.Count;

        public HankyCodeService()
		{
            HankyCodeVersion = 0;
            RecomendedFlags = new Dictionary<Guid, RecomendedFlag>();
            DoffedFlags = new Dictionary<Guid, DoffedFlag>();
            DonnedFlags = new Dictionary<Guid, DonnedFlag>();
            DonnedLeftFlags = new Dictionary<Guid, DonnedFlag>();
            DonnedRightFlags = new Dictionary<Guid, DonnedFlag>();
            FlagIDsByDescription = new Dictionary<string, Guid>();
            FlagKeys = new List<Guid>();
        }

		/// <summary>
		/// Adds a new flag to the Hanky Code Flag Library if that flag does not already exsist. 
		/// </summary>
		/// <param name="newFlag"></param>
        public void AddFlag(DoffedFlag newFlag)
        {
            
            if (!HasFlag(newFlag.ID))
			{
                Console.WriteLine("\nAdding flag to code ID: " + newFlag.ID);
                DoffedFlags.Add(newFlag.ID, newFlag);

                var donnedLeftFlag = newFlag.DonnFlag(FlaggableLocations.Left);
                var donnedRightFlag = newFlag.DonnFlag(FlaggableLocations.Right);
                DonnedFlags.Add(donnedLeftFlag.ID, donnedLeftFlag);
                DonnedFlags.Add(donnedRightFlag.ID, donnedRightFlag);
                DonnedLeftFlags.Add(newFlag.ID, donnedLeftFlag);
                DonnedRightFlags.Add(newFlag.ID, donnedRightFlag);

                FlagIDsByDescription.Add(newFlag.VisualDescription, newFlag.ID);
                FlagKeys.Add(newFlag.ID);
                Console.WriteLine("\nAdded flag(s)");
            }
        }

        /// <summary>
        /// Checks the Flag Library for the specified <paramref name="flag"/> by looking for its ID
        /// </summary>
        /// <param name="flag">The flag to be looked up</param>
        /// <returns>True if the flag is in the library and false if it is not. </returns>
        public bool HasFlag(BaseFlag flag)
		{
			return DoffedFlags.ContainsKey(flag.ID);
		}

        public bool HasFlag(Guid flagID)
        {
            return DoffedFlags.ContainsKey(flagID);
        }

        public bool HasFlag(string flagDescription)
        {
            return FlagIDsByDescription.ContainsKey(flagDescription);
        }

        public DoffedFlag GetRandomDoffedFlag()
        {
            var randIndex = new Random().Next(FlagKeys.Count);
            var randKey = FlagKeys[randIndex];
            return DoffedFlags[randKey];
        }

        /// <summary>
        /// Gets a flag by its GUID ID if it exsists. 
        /// </summary>
        /// <param name="iD">a valid GUID ID</param>
        /// <returns>The flag or <c>Null</c></returns>
        public DoffedFlag GetFlagByID(Guid iD)
        {
            if (DoffedFlags.ContainsKey(iD))
                return DoffedFlags[iD];

            return null;
        }

        public DoffedFlag GetFlagByVisualDescription(string visualDescription)
        {
            if (FlagIDsByDescription.ContainsKey(visualDescription))
            {
                var flagID = FlagIDsByDescription[visualDescription];
                return GetFlagByID(flagID);
            } 

            return null;
        }

        /// <summary>
        /// Get all the donned flags that correspond to the given donned hanky.
        /// Typicly only one is returned but it can be multiple. 
        /// </summary>
        /// <param name="donnedFlag"></param>
        /// <returns>A list of corresponding Hankies</returns>
        public List<DonnedFlag> GetCorrespondingDonnedFlags(DonnedFlag donnedFlag)
        {
            var result = new List<DonnedFlag>();

            //Check for special proccessing
            if (donnedFlag.VisualDescription == "Orange worn on the left")
            {
                result = GetAllDonnedFlags();
            } else
            {
                //Standard Matching rules
                var singleCorrespondingFlag = GetOppositeDonnedFlag(donnedFlag);
                result.Add(singleCorrespondingFlag);
            }

            return result;
        }

        /// <summary>
        /// Get all the donned flags that correspond to the given donned hankies.
        /// </summary>
        /// <remarks>
        /// This has bad BigO Notation. consider a solution with hash tables.</remarks>
        /// <param name="donnedFlags"></param>
        /// <returns>A list of corresponding Hankies</returns>
        public List<DonnedFlag> GetCorrespondingDonnedFlags(List<DonnedFlag> donnedFlags)
        {
            var result = new List<DonnedFlag>();
            foreach (var donnedFlag in donnedFlags)
            {
                var correspondingFlags = GetCorrespondingDonnedFlags(donnedFlag);
                foreach (var correspondingFlag in correspondingFlags)
                {
                    if (!result.Contains(correspondingFlag))
                    {
                        result.Add(correspondingFlag);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the <c>DonnedFlag</c> that is in the exact opposite position
        /// from the given <c>DonnedFlag</c>. This is different than getting
        /// corresponding or matching flags because no matching rules are
        /// followed here.
        /// </summary>
        /// <param name="donnedFlag">The flag that is currenly being worn</param>
        /// <returns>A single <c>DonnedFlag</c> that is in the opposite flagged
        /// position.</returns>
        public DonnedFlag GetOppositeDonnedFlag(DonnedFlag donnedFlag)
        {
            try
            {
                if (donnedFlag.Location == FlaggableLocations.Left)
                {
                    return DonnedRightFlags[donnedFlag.DoffedID];
                }
                else if (donnedFlag.Location == FlaggableLocations.Right)
                {
                    return DonnedLeftFlags[donnedFlag.DoffedID];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the <c>DonnedFlag</c>s on either the left or right side. 
        /// </summary>
        /// <param name="location">Which side to get</param>
        /// <returns>A list of <c>DonnedFlag</c>s</returns>
        public List<DonnedFlag> GetAllDonnedFlags(FlaggableLocations location)
        {
            if (location == FlaggableLocations.Left)
                return DonnedLeftFlags.Values.ToList();

            return DonnedRightFlags.Values.ToList();
        }

        /// <summary>
        /// Get all the <c>DonnedFlag</c>s from both sides. 
        /// </summary>
        /// <returns>A list of <c>DonnedFlag</c>s</returns>
        public List<DonnedFlag> GetAllDonnedFlags()
        {
            HashSet<DonnedFlag> hSet = new HashSet<DonnedFlag>(DonnedLeftFlags.Values);
            hSet.UnionWith(DonnedRightFlags.Values);
            return hSet.ToList();
        }

        /// <summary>
        /// Get a donned flag by its doffedID and location. Checks both left and right hash sets.
        /// </summary>
        /// <param name="doffedID">The GUID of the doffed flag</param>
        /// <returns>A DonnedFlag. Can return null</returns>
        public DonnedFlag GetDonnedFlagByDoffedID(Guid doffedID, FlaggableLocations locations)
        {
            try
            {
                if (locations == FlaggableLocations.Left)
                {
                    return DonnedLeftFlags[doffedID];
                }
                else
                {
                    return DonnedRightFlags[doffedID];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DonnedFlag GetDonnedFlagByID(Guid iD)
        {
            if (DonnedFlags.ContainsKey(iD))
                return DonnedFlags[iD];

            return null;
        }

        /// <summary>
        /// Recomend a new flag to the Hanky Code that is not already in the
        /// code or has been recomended. 
        /// </summary>
        /// <param name="newFlag">The new flag to be recomended. Its ID is what
        /// determines if its already in use.</param>
        public void RecomendNewFlagToHankyCode(RecomendedFlag newFlag)
        {
            if (!RecomendedFlags.ContainsKey(newFlag.ID) &&
                !DoffedFlags.ContainsKey(newFlag.ID))
                RecomendedFlags.Add(newFlag.ID, newFlag);
        }

        /// <summary>
        /// Gets all the recomended flags.
        /// </summary>
        /// <returns>A list of recomended flags</returns>
        public List<RecomendedFlag> GetRecomendedFlags()
        {
            return RecomendedFlags.Values.ToList();
        }

        public HankyCodeModel ToHankyCodeModel()
        {
            var flagModels = new List<FlagJSONModel>();
            foreach (var flag in DoffedFlags.Values)
            {
                flagModels.Add(flag.ToModel());
            }
            Console.WriteLine("Model Flags - " + flagModels.Count);

            return new HankyCodeModel()
            {
                Flags = flagModels,
                //RecomendedFlags = this.RecomendedFlags,
                Version = HankyCodeVersion
            };
        }

        

        public void SaveHankyCode(string fileName = "HankyCode.JSON")
        {
            HankyCodeVersion++;
                var hankyCodeModel = ToHankyCodeModel();
                Console.WriteLine("Created Hanky Model");

                var hankyCodeString = hankyCodeModel.ToJSON();
            Console.WriteLine("\nHanky Code JSON:\n\n" + hankyCodeString +"\n");

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                fileName);
            File.WriteAllText(path, hankyCodeString);

        }


    }
}

