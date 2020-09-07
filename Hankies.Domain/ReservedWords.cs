using System;

namespace Hankies.Domain
{
    /// <summary>
    /// Contains a static array of reserved words in Hankies. 
    /// </summary>
    /// <remarks>
    /// These are words that beling to the Hankies brand or may be a security
    /// issue</remarks>
    public static class ReserverdWords
    {
        public static readonly string[] ReservedWords =
        {
            "Hankies",
            "Admin",
            "Official"
        };

        /// <summary>
        /// Combines all the reserved words into a pattern
        /// </summary>
        /// <returns>A RegEx pattern suitable</returns>
        public static string Pattern()
        {
            return string.Join('|', ReservedWords);
        }
    }
}
