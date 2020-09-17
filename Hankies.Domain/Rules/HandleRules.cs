using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Rules
{
    /// <summary>
    /// Rules that Handles in Hankies must follow. 
    /// </summary>
    public static class HandleRules
    {
        static int HandleMinLength => 6;

        static int HandleMaxLength => 50;

        static string HandleAllowedCharacters => "^(a-zA-Z0-9_)";

        /// <summary>
        /// Check a string for any Handle rule violations. 
        /// </summary>
        /// <param name="handle">The string to check for rule violations</param>
        /// <returns></returns>
        public static IEnumerable<HankiesRuleViolation> GetHandleRuleViolations
            (string handle)
        {
            // Load regex filters. 
            Regex reservedWordFilter = new Regex(ReserverdWords.Pattern());
            Regex allowedCharactersFilter = new Regex(HandleAllowedCharacters);

            if (reservedWordFilter.IsMatch(handle))
                yield return new HankiesRuleViolation
                    ("Handle can't match a reserved word.", "Handle");
            
            if (handle.Length < HandleMinLength)
                yield return new HankiesRuleViolation("Handle is to short.",
                    "Handle");

            if (handle.Length > HandleMaxLength)
                yield return new HankiesRuleViolation("Handle to long.",
                    "Handle");

            if (!allowedCharactersFilter.IsMatch(handle))
                yield return new HankiesRuleViolation
                    ("Handle contains invalid characters", "Handle");

            yield break;
        }
    }
}
