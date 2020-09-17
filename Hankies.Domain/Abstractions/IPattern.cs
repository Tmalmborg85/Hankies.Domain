using System;
namespace Hankies.Domain.Models.Abstractions
{
    public interface IPattern
    {
        /// <summary>
        /// The name of this pattern. 
        /// </summary>
        /// <example>
        /// checker, red stripe
        /// </example>
        public string Name { get; }

        /// <summary>
        /// The public address of this image. 
        /// </summary>
        public Uri Location { get; }

        /// <summary>
        /// Detail color of this pattern.
        /// </summary>
        public INamedColor Color { get; }

        /// <summary>
        /// Update this pattern's Uri pointer. 
        /// </summary>
        /// <param name="newUri"></param>
        /// <returns>An IStatus with pattern</returns>
        IStatus<IPattern> UpdateUri(Uri newUri);

        /// <summary>
        /// Update this pattern's detail color with a valid color
        /// </summary>
        /// <param name="newColor">The new color detail.</param>
        /// <returns></returns>
        IStatus<IPattern> UpdateColor(INamedColor newColor);

        /// <summary>
        /// Remove the color from this pattern.
        /// </summary>
        void RemoveColor();
    }
}
