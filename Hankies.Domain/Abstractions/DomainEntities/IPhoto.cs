﻿using System;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface IPhoto : IDeletableDomainEntity, IReportableContent
    {
        public  CCustomer Owner { get; }
        public Uri Location { get; }
        public DateTimeOffset RatingUpdatedAt { get; }
        public PhotoRatings Rating { get; }

        void UpdateRating(PhotoRatings newRating);
    }
}
