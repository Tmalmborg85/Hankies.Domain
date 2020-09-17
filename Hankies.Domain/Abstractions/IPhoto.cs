using System;
namespace Hankies.Domain.Models.Abstractions
{
    public interface IPhoto : IEntity
    {
        public ICustomer Owner { get; }
        public Uri Location { get; }
        public DateTimeOffset RatingUpdatedAt { get; }
        public PhotoRatings Rating { get; }

        void UpdateRating(PhotoRatings newRating);
    }
}
