namespace Hankies.Domain.Models.Abstractions
{

    public interface IHandle : IValueObject
    {
        /// <summary>
        /// A human readable string indicating what others should call you.
        /// </summary>
        /// <example>
        /// Daddy, Pig, Gristle McThornbody</example>
        public string Handle { get; }

        /// <summary>
        /// Sets <c>Handle</c> to a value that follows Hankie's handle rules. 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns>A status indicating if setting the handle worked.
        /// </returns>
        IStatus<string> SetHandleTo(string handle);
    }
}