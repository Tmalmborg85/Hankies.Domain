using System;
namespace Hankies.Domain.HankyCode
{

	public static class Extensions
	{
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            source = source.ToLower();
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }
    }
}

