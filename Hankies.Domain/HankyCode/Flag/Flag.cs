﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Fetish;

namespace Hankies.Domain.HankyCode.Flag
{
    public enum FlaggableLocations
    {
        Left,
        Right
    }
    public abstract class BaseFlag
    {
        public BaseFlag(string idSeed, AssociatedTrait trait)
        {
            id = GenerateID(idSeed);
            Trait = trait;
        }

        public BaseFlag(BaseFlag baseFlag)
        {
            id = baseFlag.ID;
            Trait = baseFlag.Trait;


        }

        /// <summary>
        /// Generate a GUID ID that is unique based on unique
        /// information from the flag. 
        /// </summary>
        /// <param name="idSeed">What to seed the GUID generator with.
        /// This should be unique information to the flag, like its visual description</param>
        public Guid GenerateID(string idSeed)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idSeed))
                    throw new ArgumentException(idSeed);

                idSeed = RemoveWhitespace(idSeed);

                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(idSeed));
                    return new Guid(hash);
                }
            }
            catch (Exception ex)
            {
                throw new FailedToCreateFlagIDException(ex, idSeed);
            }
        }

        public void SetVisualDescription(string rawDescription)
        {
            VisualDescription = rawDescription.ToUpperFirstLetter();
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string RemoveWhitespace(string input)
        {
            return sWhitespace.Replace(input, "");
        }

        /// <summary>
        /// Gets the opposite of the provided flaggable location.
        /// <remarks>
        /// This only works in a system where flaggable locations are only left/right
        /// </remarks>
        /// </summary>
        /// <param name="locations">Left or Right</param>
        /// <returns>Left or Right</returns>
        public FlaggableLocations GetOppositeLocation(FlaggableLocations locations)
        {
            if (locations == FlaggableLocations.Left)
                return FlaggableLocations.Right;

            return FlaggableLocations.Left;
        }

        private Guid id = Guid.Empty;
        /// <summary>
        /// Each flag has a unique ID based on its unique description.
        /// </summary>
        public Guid ID => id;

        public AssociatedTrait Trait { get;}

        public string VisualDescription { get; private set; }

        //flag needs someway to define matching rules??? well, only one flag breaks the standard match rules

        /// <summary>
        /// Create a donned flag from this flag. 
        /// </summary>
        /// <param name="location">The side the flag is going to be worn on</param>
        /// <returns>A <c>DonnedFlag</c> or Null if an error happens.</returns>
        public DonnedFlag DonnFlag(FlaggableLocations location)
        {
            try
            {
                return new DonnedFlag(this, location);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
