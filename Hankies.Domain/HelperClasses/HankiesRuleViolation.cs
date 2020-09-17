using System;
namespace Hankies.Domain.HelperClasses
{
    /// <summary>
    /// A violation of a hankies bussiness rule
    /// </summary>
    public class HankiesRuleViolation
    {
        /// <summary>
        /// The business rule that was violated.
        /// </summary>
        /// <example>
        /// "An avatar session must be owned by an avatar",
        /// "Handles must be at least 4 characters long" 
        /// </example>
        public string Rule { get; }

        /// <summary>
        /// Who, object or property, broke the rule. 
        /// </summary>
        public string Violator { get; }

        /// <summary>
        /// Construct a Rule Violation from a rule and a named violator.
        /// </summary>
        /// <param name="rule">The business rule that was broken.</param>
        /// <param name="violator">Who broke the busines rule</param>
        public HankiesRuleViolation(string rule, string violator)
        {
            if (string.IsNullOrEmpty(rule) || string.IsNullOrWhiteSpace(rule))
                throw new Exception(nameof(rule) + " is required.");

            if (string.IsNullOrEmpty(violator) || string.IsNullOrWhiteSpace(violator))
                throw new Exception(nameof(violator) + " is required.");

            Rule = rule;
            Violator = violator;
        }

        /// <summary>
        /// Construct a Rule Violation from a rule and an object. 
        /// </summary>
        /// <param name="rule">The business rule that was broken.</param>
        /// <param name="violator">Who broke the busines rule</param>
        public HankiesRuleViolation(string rule, object violator) : this
            (rule, nameof(violator)) { }
    }
}
