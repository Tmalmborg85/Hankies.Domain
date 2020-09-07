using System;
using System.Text.RegularExpressions;
using Hankies.Domain.Exceptions;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    public class NamedColor : INamedColor
    {
        /*
         * Valid Hex Rules
            1. 123456 – must start with a “#” symbol
            2. #afafah – “h” is not allow, valid letter from “a” to “f”
            3. #123abce – either 6 length or 3 length
            4. aFaE3f – must start with a “#” symbol, either 6 length or 3 length
            5. F00 – must start with a “#” symbol
            6. #afaf – either 6 length or 3 length
            7. #F0h – “h” is not allow, valid letter from “a” to “f”
        */
        const string validHexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

        // A private contructor for ORM mappers like EF Core and serializers
        private NamedColor() { }

        /// <summary>
        /// A name and hex value for a color
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hexValue"></param>
        public NamedColor(string name, string hexValue)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(name, "Colors must have a name");

            if (!IsValidHex(hexValue))
                throw new InvalidColorHexException(hexValue);

            Name = name;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public IStatus<string> UpdateHex(string newHex)
        {
            Status<string> updateStatus = new Status<string>();

            if (!IsValidHex(newHex))
            {
                updateStatus.AddException(new InvalidColorHexException(newHex));
            }
            else
            {
                updateStatus.RespondWith(newHex);
            }

            return updateStatus;
        }

        public FontColorTypes UseDarkOrLightTextColor()
        {
            float luma = System.Drawing.ColorTranslator.FromHtml(Value)
                .GetBrightness();

            // Return black for bright colors, white for dark colors
            return luma > 0.5 ? FontColorTypes.Dark : FontColorTypes.Light;
        }

        /// <summary>
        /// checks a hexValue against a regular expression to see if it is
        /// valid. 
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        private bool IsValidHex(string hexValue)
        {
            Regex rgx = new Regex(validHexPattern);
            return rgx.IsMatch(hexValue);
        }
    }
}
