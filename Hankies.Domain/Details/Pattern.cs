using System;
using System.Globalization;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class Pattern : IPattern
    {
        // A private contructor for ORM mappers like EF Core and serializers
        public Pattern() { }

        public Pattern(string name, Uri location, INamedColor color)
        {
            // Pattern name's must be tite case
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase
                (name.ToLower());
            
            Location = location;

            Color = color;
        }

        public string Name { get; private set; }

        public Uri Location { get; private set; }

        public INamedColor Color { get; private set; }

        public void RemoveColor()
        {
            Color = null;
        }

        public IStatus<IPattern> UpdateColor(INamedColor newColor)
        {
            var updatePattern = new Status<IPattern>(true);
            if (newColor == null)   
            {
                updatePattern.AddError("New color cant be null. To remove a color " +
                    "use RemoveColor()");
            }
            updatePattern.RespondWith(this);

            return updatePattern;
        }

        public IStatus<IPattern> UpdateUri(Uri newUri)
        {
            var updateStatus = new Status<IPattern>(true);
            if (newUri == null)
            {
                updateStatus.AddError("Can't set location as a null URL");
            }
            updateStatus.RespondWith(this);

            return updateStatus;
        }
    }
}
