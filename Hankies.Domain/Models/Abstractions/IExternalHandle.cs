using System;
namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A human friendly and reputable link to an external user profile.
    /// </summary>
    public interface IExternalHandle : IHandle
    {
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
