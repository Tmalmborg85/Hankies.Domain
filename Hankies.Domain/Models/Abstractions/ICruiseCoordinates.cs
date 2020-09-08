using System;
namespace Hankies.Domain.Models.Abstractions
{
    public interface ICruiseCoordinates
    {
        public double Lattitude { get; set; }
        public double Longitude { get; }

    }
}
