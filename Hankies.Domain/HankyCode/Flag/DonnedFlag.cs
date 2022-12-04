using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hankies.Domain.HankyCode.Flag
{
	/*
	 * I'm having an internal argument about if I should maintain a list of 
	 * matching flags here or not. From what I remember of Domain Drivin Design 
	 * it would make sense that the model contain that information. In the real 
	 * world a worn hanky explicitly indicates a trait and roll and implies a 
	 * matching trait and roll or traits and rolls. 
	 * 
	 * However... it just seems like whats the point of the HankyCodeService 
	 * then if translation is going to essentially be happening on each flag. 
	 * that isnt a very solid argument so I'm going with maintaining a list of 
	 * corresponding worn hankies. 
	 * */
	public class DonnedFlag : BaseFlag, IDonned
    {

        public Guid DonnedID { get; set; }
        public FlaggableLocations Location { get; set; }

        /// <summary>
        /// The <c>DonnedFlag</c> that explicitly corresponds to this hanky.
        /// </summary>
        //public Guid CorrespondingDonnedFlagID { get; set; }

        public DonnedFlag(BaseFlag flag, FlaggableLocations location) : base(flag)
		{
			SetVisualDescription(BuildVisualDescription(flag.VisualDescription, location));
            Location = location;

            GenerateFlagIDs(flag.VisualDescription, location);
        }

        private string BuildVisualDescription(string flagDescription, FlaggableLocations location)
        {
            return flagDescription + " worn on the " + location.ToString();
        }

        private void GenerateFlagIDs(string description, FlaggableLocations location)
        {
            var matchingFlagVisualDescription = BuildVisualDescription(description, GetOppositeLocation(location));

            DonnedID = GenerateID(VisualDescription);
            //CorrespondingDonnedFlagID = GenerateID(matchingFlagVisualDescription.ToUpperFirstLetter());

        }
    }
}

