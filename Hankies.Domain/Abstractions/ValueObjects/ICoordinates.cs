using System;

namespace Hankies.Domain.Abstractions.ValueObjects
{
    public interface ICoordinates
    {
        public double Lattitude { get; }
        public double Longitude { get; }
    }
}
