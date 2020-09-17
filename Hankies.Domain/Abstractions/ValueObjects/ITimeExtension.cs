﻿
namespace Hankies.Domain.Abstractions.ValueObjects
{
    /// <summary>
    /// A purchasable extension of time. 
    /// </summary>
    public interface ITimeExtension : ITokenProduct, IValueObject
    {
        public int Minutes { get; }
    }
}