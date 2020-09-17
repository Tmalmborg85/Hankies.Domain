using System;
using Hankies.Domain.Details.DomainEntities;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface IPattern : IDomainEntity
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
        public NamedColor Color { get; }

        /// <summary>
        /// Update this pattern's Uri pointer. 
        /// </summary>
        /// <param name="newUri"></param>
        /// <returns>An IStatus with pattern</returns>
        public IStatus<IPattern> UpdateUri(Uri newUri);

        /// <summary>
        /// Update this pattern's detail color with a valid color
        /// </summary>
        /// <param name="newColor">The new color detail.</param>
        /// <returns></returns>
        public IStatus<IPattern> UpdateColor(NamedColor newColor);

        /// <summary>
        /// Remove the color from this pattern.
        /// </summary>
        public void RemoveColor();
    }
}
