using System;
namespace Hankies.Domain.Abstractions.ValueObjects
{
    public interface ICruiseCoordinates : IValueObject
    {
        public double Lattitude { get; set; }
        public double Longitude { get; }

    }
}
