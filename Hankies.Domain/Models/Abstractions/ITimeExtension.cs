namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A purchasable extension of time. 
    /// </summary>
    public interface ITimeExtension : ITokenProduct, IValueObject
    {
        public int Minutes { get; }
    }
}