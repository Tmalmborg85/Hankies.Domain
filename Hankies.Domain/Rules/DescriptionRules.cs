using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Rules
{
    /// <summary>
    /// Rules that self description's must follow.
    /// </summary>
    public static class DescriptionRules
    {
        static int MinCharacters => 30;

        static int MaxCharacters => 140;

        static int MinWords => 10;

        static string AllowedCharacters => "^(a-zA-Z0-9_)";

        public static bool IsValid(string description)
        {
            return GetRuleViolations(description).Count() == 0; 
        }

        public static IEnumerable<HankiesRuleViolation> GetRuleViolations
            (string description)
        {
            // Load regex filters. 
            Regex reservedWordFilter = new Regex(ReserverdWords.Pattern());
            Regex allowedCharactersFilter = new Regex(AllowedCharacters);

            if (reservedWordFilter.IsMatch(description))
                yield return new HankiesRuleViolation
                    ("Description can't match a reserved word."
                    , "Description");

            if (description.Length < MinCharacters)
                yield return new HankiesRuleViolation
                    ("Description does not have enough characters."
                    , "Description");

            if (description.Length > MaxCharacters)
                yield return new HankiesRuleViolation
                    ("Description has too many characters."
                    , "Description");

            if (description.Split(' ').Length < MinWords)
                yield return new HankiesRuleViolation
                    ("Descriptions must be 10 or more words long"
                    , "Description");

            if (!allowedCharactersFilter.IsMatch(description))
                yield return new HankiesRuleViolation
                    ("Description contains invalid characters"
                    , "Description");

            yield break;
        }
    }
}
