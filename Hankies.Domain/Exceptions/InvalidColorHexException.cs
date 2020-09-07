using System;
namespace Hankies.Domain.Exceptions
{
    public class InvalidColorHexException : ArgumentOutOfRangeException
    {
        public InvalidColorHexException(string hexValue) : base(hexValue,
            hexValue, "An invalid color hex value was provided.")
        {
        }
    }
}
