using Hankies.Domain.Models.Abstractions.Validators;

namespace Hankies.Domain.Models.Abstractions
{

    public interface IHandle : IValueObject, IHandleValidator
    {
        /// <summary>
        /// A human readable string indicating what others should call you.
        /// </summary>
        /// <example>
        /// Daddy, Pig, Gristle McThornbody</example>
        public string Handle { get; }
    }
}