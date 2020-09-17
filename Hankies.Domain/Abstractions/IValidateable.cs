using System.Collections.Generic;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Abstractions
{
    /// <summary>
    /// An object that can be validated by business rules. 
    /// </summary>
    public interface IValidateable
    {
        /// <summary>
        /// Does this domain entity follow all its business rules?
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Checks for any business rule violations. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HankiesRuleViolation> GetRuleViolations();

        /// <summary>
        /// Throw a validation error if this entity is not valid. 
        /// </summary>
        public void OnValidate();
    }
}
