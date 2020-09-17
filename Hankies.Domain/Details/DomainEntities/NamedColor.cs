using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.HelperClasses;

namespace Hankies.Domain.Details.DomainEntities
{
    public class NamedColor : INamedColor
    {

        // A private contructor for ORM mappers like EF Core and serializers
        private NamedColor() { }

        /// <summary>
        /// A name and hex value for a color
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hexValue"></param>
        public NamedColor(string name, string hexValue)
        {
            Name = name;
            Value = hexValue;
            
            OnValidate();
        }

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
        [NotMapped]
        const string validHexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

        [NotMapped]
        const int ColorNameMinLength = 3;

        [NotMapped]
        const int ColorNameMaxLength = 50;

        [Required, MinLength(ColorNameMinLength), MaxLength(ColorNameMaxLength)]
        public string Name { get; private set; }

        [Key, Required]
        public string Value { get; private set; }

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public FontColorTypes UseDarkOrLightTextColor()
        {
            float luma = System.Drawing.ColorTranslator.FromHtml(Value)
                .GetBrightness();

            // Return black for bright colors, white for dark colors
            return luma > 0.5 ? FontColorTypes.Dark : FontColorTypes.Light;
        }

        /// <inheritdoc cref="IDomainEntity.GetRuleViolations"/>
        public IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                yield return new HankiesRuleViolation
                    ("Colors must have a name.", Name);

            if (Name != textInfo.ToTitleCase(Name))
                yield return new HankiesRuleViolation
                    ("Color names must be title case.", Name);

            if (Name.Length < ColorNameMinLength)
                yield return new HankiesRuleViolation("Color name is to short.",
                    Name);

            if (Name.Length > ColorNameMaxLength)
                yield return new HankiesRuleViolation("Color name is to long.",
                    Name);

            Regex validHexRegEx = new Regex(validHexPattern);

            if (string.IsNullOrEmpty(Value) || string.IsNullOrWhiteSpace(Value))
                yield return new HankiesRuleViolation("Cant be empty", Value);

            if (!validHexRegEx.IsMatch(Value))
                yield return new HankiesRuleViolation
                    ("Not in a valid hex format", Value);
            yield break;
        }

        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
