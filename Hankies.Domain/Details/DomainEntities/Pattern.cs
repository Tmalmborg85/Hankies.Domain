using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Details.DomainEntities
{
    public class Pattern : DomainEntity, IPattern
    {
        // A private contructor for ORM mappers like EF Core and serializers
        private Pattern(Guid id, DateTimeOffset createdAt) : base
            (id, createdAt) { }

        /// <summary>
        /// Creates and then validates a Hankies Pattern
        /// </summary>
        /// <param name="id">This pattern's unique ID</param>
        /// <param name="createdAt">When this pattern was originaly created
        /// </param>
        /// <param name="name">A name for this patter that is dispayed to
        /// customers</param>
        /// <param name="location">The URI location of this pattern's SVG image</param>
        /// <param name="color">If this pattern has a color detail that should
        /// be appiled to the SVG.</param>
        public Pattern(Guid id, DateTimeOffset createdAt, string name,
            Uri location, NamedColor color) : base (id, createdAt)
        {
            Name = name;
            Location = location;
            Color = color;

            OnValidate();
        }

        /// <summary>
        /// Creates and then validates a Hankies Pattern
        /// </summary>
        /// <param name="id">This pattern's unique ID</param>
        /// <param name="createdAt">When this pattern was originaly created
        /// </param>
        /// <param name="name">A name for this patter that is dispayed to
        /// customers</param>
        /// <param name="location">The URI location of this pattern's SVG image</param>
        public Pattern(Guid id, DateTimeOffset createdAt, string name,
            Uri location) : base(id, createdAt)
        {
            Name = name;
            Location = location;
            Color = null;

            OnValidate();
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        public Uri Location { get; private set; }

        public NamedColor Color { get; private set; }

        public void RemoveColor()
        {
            Color = null;
        }

        public IStatus<IPattern> UpdateColor(NamedColor newColor)
        {
            Color = newColor;
            var updatePattern = new Status<IPattern>(true);
            
            if (!IsValid)
                updatePattern.AddError("Updated color had rule violations");

            updatePattern.RespondWithObject(this);
            return updatePattern;
        }

        public IStatus<IPattern> UpdateUri(Uri newUri)
        {
            Location = newUri;
            var updateStatus = new Status<IPattern>(true);

            if (!IsValid)
                updateStatus.AddError("Updated uri had rule violations");

            updateStatus.RespondWithObject(this);
            return updateStatus;
        }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            if (Color == null)
                yield return new HankiesRuleViolation("Color cant be null",
                    Color);

            if (Location == null)
                yield return new HankiesRuleViolation("Location cant be null.",
                    Location);

            if (Color != null && !Color.IsValid)
                yield return new HankiesRuleViolation("Detail color can't be " +
                    "invalid", Color);

            yield break;
        }
    }
}
