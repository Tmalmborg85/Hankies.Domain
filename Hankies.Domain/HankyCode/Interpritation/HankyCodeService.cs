using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.HankyCode.Flag;

namespace Hankies.Domain.HankyCode.Interpritation
{
	public class HankyCodeService
	{
		private Dictionary<Guid, BaseFlag> Flags { get; set; }
        private Dictionary<Guid, DonnedFlag> DonnedLeftFlags { get; set; }
        private Dictionary<Guid, DonnedFlag> DonnedRightFlags { get; set; }
        private Dictionary<string, Guid> FlagIDsByDescription { get; set; }
        private List<Guid> FlagKeys { get; set; }

        public HankyCodeService()
		{
            Flags = new Dictionary<Guid, BaseFlag>();
            FlagIDsByDescription = new Dictionary<string, Guid>();
            FlagKeys = new List<Guid>();
        }

		/// <summary>
		/// Adds a new flag to the Hanky Code Flag Library if that flag does not already exsist. 
		/// </summary>
		/// <param name="newFlag"></param>
        public void AddFlag(BaseFlag newFlag)
        {
            if (!HasFlag(newFlag))
			{
				Flags.Add(newFlag.ID, newFlag);
                DonnedLeftFlags.Add(newFlag.ID, newFlag.DonnFlag(FlaggableLocations.Left));
                DonnedRightFlags.Add(newFlag.ID, newFlag.DonnFlag(FlaggableLocations.Right));
                FlagIDsByDescription.Add(newFlag.VisualDescription, newFlag.ID);
                FlagKeys.Add(newFlag.ID);
            }
        }

        /// <summary>
        /// Checks the Flag Library for the specified <paramref name="flag"/> by looking for its ID
        /// </summary>
        /// <param name="flag">The flag to be looked up</param>
        /// <returns>True if the flag is in the library and false if it is not. </returns>
        public bool HasFlag(BaseFlag flag)
		{
			return Flags.ContainsKey(flag.ID);
		}

        public bool HasFlag(Guid flagID)
        {
            return Flags.ContainsKey(flagID);
        }

        public bool HasFlag(string flagDescription)
        {
            return FlagIDsByDescription.ContainsKey(flagDescription);
        }

        public BaseFlag GetRandomFlag()
        {
            var randIndex = new Random().Next(FlagKeys.Count);
            var randKey = FlagKeys[randIndex];
            return Flags[randKey];
        }

        /// <summary>
        /// Gets a flag by its GUID ID if it exsists. 
        /// </summary>
        /// <param name="iD">a valid GUID ID</param>
        /// <returns>The flag or <c>Null</c></returns>
        public BaseFlag GetFlagByID(Guid iD)
        {
            if (Flags.ContainsKey(iD))
                return Flags[iD];

            return null;
        }

        public BaseFlag GetFlagByVisualDescription(string visualDescription)
        {
            if (FlagIDsByDescription.ContainsKey(visualDescription))
            {
                var flagID = FlagIDsByDescription[visualDescription];
                return GetFlagByID(flagID);
            } 

            return null;
        }

        public List<DonnedFlag> GetCorrespondingDonnedFlags(DonnedFlag lightBlueHankyWornOnLeft)
        {
            var result = new List<DonnedFlag>();

            //Check for special proccessing
            if (UsesNonStandardMatchingRules(lightBlueHankyWornOnLeft.ID))
            {

            } else
            {
                //Standard Matching rules
                var singleCorrespondingFlag = GetOppositeDonnedFlag(lightBlueHankyWornOnLeft);
                result.Add(singleCorrespondingFlag);
            }

            return result;
        }

        private bool UsesNonStandardMatchingRules(Guid iD)
        {
            return false;
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
            if (donnedFlag.Location == FlaggableLocations.Left)
            {
                return DonnedLeftFlags[donnedFlag.ID];
            } else if (donnedFlag.Location == FlaggableLocations.Right)
            {
                return DonnedRightFlags[donnedFlag.ID];
            } else
            {
                return null;
            }
        }

        public List<DonnedFlag> GetAllDonnedFlags()
        {
            HashSet<DonnedFlag> hSet = new HashSet<DonnedFlag>(DonnedLeftFlags.Values);
            hSet.UnionWith(DonnedRightFlags.Values);
            return hSet.ToList();
        }
    }
}

