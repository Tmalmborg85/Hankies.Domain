using System;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Models.Details
{
    /// <summary>
    /// A handle that leads to a trusted external profile.
    /// </summary>
    /// <example>
    /// Facebook, twitter</example>
    public class ExternalHandle : Handle, IExternalHandle
    {
        public ExternalHandle(string handle, Uri link,
            TrustedPlatforms platform) : base(handle)
        {
            var validHandleStatus = ValidateHandle(handle);
            if (!validHandleStatus.IsSuccess())
            {
                // Cant procede. Get & throw errors that caused failure. 
                throw new ArgumentOutOfRangeException(handle, handle
                    , validHandleStatus.ErrorMessage);
            }

            Link = link;
            Platform = platform;
        }

        /// <summary>
        /// Deep link to external user account
        /// </summary>
        public Uri Link { get; private set; }

        /// <summary>
        /// The trusted platform that the account belonegs to. 
        /// </summary>
        public TrustedPlatforms Platform { get; private set; }
    }
}
