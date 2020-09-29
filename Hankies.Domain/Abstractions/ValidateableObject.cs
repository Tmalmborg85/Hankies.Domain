using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Abstractions
{
    public abstract class ValidateableObject : IValidateable
    {
        public abstract IEnumerable<HankiesRuleViolation> GetRuleViolations();

        public bool IsValid
        {
            get { return GetRuleViolations().Count() == 0; }
        }

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
