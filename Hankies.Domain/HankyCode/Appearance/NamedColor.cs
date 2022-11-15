using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Hankies.Domain.HankyCode.Appearance
{
    public class NamedColor
    {
        /// <summary>
        /// A name and hex value for a color
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hexValue"></param>
        public NamedColor(string name, string hexValue)
        {
            Name = name;

            Regex validHexRegEx = new Regex(validHexPattern);
            if (validHexRegEx.IsMatch(hexValue))
                Hex = hexValue;
            
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
        const string validHexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

        const int ColorNameMinLength = 3;

        const int ColorNameMaxLength = 50;

        [Required, MinLength(ColorNameMinLength), MaxLength(ColorNameMaxLength)]
        public string Name { get; private set; }

        [Key, Required]
        public string Hex { get; private set; }

        
    }
}
