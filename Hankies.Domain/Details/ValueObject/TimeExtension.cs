
using Hankies.Domain.Abstractions.ValueObjects;

namespace Hankies.Domain.Details.ValueObjects
{
    /// <summary>
    /// A purchasable extension of time. 
    /// </summary>
    public class TimeExtension : ITokenProduct, IValueObject
    {
        public int Minutes { get; private set; }

        public int TokenCost { get; private set; }

        public string ProductName { get; private set; }
    }
}