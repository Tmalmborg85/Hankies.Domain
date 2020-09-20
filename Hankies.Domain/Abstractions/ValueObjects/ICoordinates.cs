using System;

namespace Hankies.Domain.Abstractions.ValueObjects
{
    public interface ICoordinates : IValueObject
    {
        public double Lattitude { get; }
        public double Longitude { get; }
    }
}
