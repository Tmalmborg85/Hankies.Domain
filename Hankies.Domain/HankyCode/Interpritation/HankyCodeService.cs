using System;
using System.Collections.Generic;
using Hankies.Domain.HankyCode.Flag;

namespace Hankies.Domain.HankyCode.Interpritation
{
	public class HankyCodeService
	{
		private Dictionary<Guid, BaseFlag> Flags { get; set; }
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
        public object GetFlagByID(Guid iD)
        {
            if (Flags.ContainsKey(iD))
                return Flags[iD];

            return null;
        }

        public object GetFlagByVisualDescription(string visualDescription)
        {
            if (FlagIDsByDescription.ContainsKey(visualDescription))
            {
                var flagID = FlagIDsByDescription[visualDescription];
                return GetFlagByID(flagID);
            } 

            return null;
        }
    }
}

