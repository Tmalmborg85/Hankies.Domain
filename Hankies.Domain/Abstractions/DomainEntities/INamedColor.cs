namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A hex color with a human friendly name. 
    /// </summary>
    public interface INamedColor : IValidateable
    {
        /// <summary>
        /// A distinct human friendly name for the color. Could be used as a
        /// key in a database.
        /// </summary>
        /// <example>
        /// Sky Blue
        /// </example>
        public string Name { get; }

        /// <summary>
        /// The distict hex value of a color.
        /// </summary>
        /// <example>
        /// #0F0F0F0
        /// </example>
        public string Value { get; }

        /// <summary>
        /// Determines if dark or light font is easiest to read when overlayed
        /// on this color. 
        /// </summary>
        /// <returns></returns>
        FontColorTypes UseDarkOrLightTextColor();
    }
}
