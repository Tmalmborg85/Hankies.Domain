using System;
namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A human friendly and reputable link to an external user profile.
    /// </summary>
    public interface IExternalHandle : IValueObject
    {
        /// <summary>
        /// Human friendly handle
        /// </summary>
        public string Handle { get; }

        /// <summary>
        /// Reputable link to an external user profile.
        /// </summary>
        public Uri Link { get; }

        /// <summary>
        /// The platform this link lead to
        /// </summary>
        public TrustedPlatforms Platform { get; }
    }
}
