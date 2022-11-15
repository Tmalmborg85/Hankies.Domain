using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    /// <summary>
    /// Hankies can come in a varriaty of appearances but they all share some common ground. 
    /// </summary>
    public abstract class Appearance
    {

        public Appearance()
        {
        }

        /// <summary>
        /// Each appearance is identifiable by its unique description.
        /// </summary>
        /// <remarks>
        /// The ID is the description with spaces removed and all capitalized. That way they can all be compared in the same format. 
        /// </remarks>
        public string ID => Description.Replace(" ", string.Empty).ToUpper();

        public abstract string Description { get; }
    }
}
