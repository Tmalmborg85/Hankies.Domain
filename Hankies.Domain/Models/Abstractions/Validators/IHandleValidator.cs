using System;
namespace Hankies.Domain.Models.Abstractions.Validators
{
    /// <summary>
    /// Validate handle rules
    /// </summary>
    public interface IHandleValidator
    {
        /// <summary>
        /// Minimum length for a user generated handle.
        /// </summary>
        int HandleMinLength { get; }

        /// <summary>
        /// Maximum length for a user generated handle.
        /// </summary>
        int HandleMaxLength { get; }

        /// <summary>
        /// Characters that can be in a user generated handle.
        /// </summary>
        string HandleAllowedCharacters { get; }

        /// <summary>
        /// Validates that a handle matches all the bussiness logic rules.  
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        IStatus<string> ValidateHandle(string handle);
    }
}
