using System;
namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface IDeletableDomainEntity
    {
        public DateTimeOffset DeletedAt { get; }
        public bool Deleted { get; set; }
    }
}
