using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Details.DomainEntities
{
    public class CustomerPhoto : DomainEntity, IPhoto
    {
        public CustomerPhoto()
        {
        }

        public ICustomer Owner => throw new NotImplementedException();

        public Uri Location => throw new NotImplementedException();

        public DateTimeOffset RatingUpdatedAt => throw new NotImplementedException();

        public PhotoRatings Rating => throw new NotImplementedException();

        public DateTimeOffset? DeletedAt => throw new NotImplementedException();

        public bool Deleted => throw new NotImplementedException();

        public IEnumerable<ICommunityReport> Reports => throw new NotImplementedException();

        public IStatus<IReportableContent> AddNewReport(ICommunityReport report)
        {
            throw new NotImplementedException();
        }

        public void DeletedEntity(DateTimeOffset deletedTimestamp)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<HankiesRuleViolation> GetRuleViolations()
        {
            throw new NotImplementedException();
        }

        public void UpdateRating(PhotoRatings newRating)
        {
            throw new NotImplementedException();
        }
    }
}
