using System;
using System.Collections.Generic;
using Hankies.Domain.Abstractions.ValueObjects;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface IExpierable<T>
    {
        public DateTimeOffset ExpiredAt { get; }

        /// <summary>
        /// Collection of time extensions. can be empty. 
        /// </summary>
        IEnumerable<ITimeExtension> TimeExtensions { get; }

        public void Expire();
        IStatus<T> ExtendCruiseTime(TimeExtensionOptions time);
    }
}
