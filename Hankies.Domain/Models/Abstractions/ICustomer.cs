using System;
using System.Collections.Generic;
using Hankies.Domain.Models.Details;

namespace Hankies.Domain.Models.Abstractions
{
    public interface ICustomer : IVoter
    {
        /// <summary>
        /// All the photos this customer owns.
        /// </summary>
        public IEnumerable<IPhoto> Photos { get; }

        /// <summary>
        /// All the avatars this customer has created.
        /// </summary>
        public IEnumerable<IAvatar> Avatars { get; }

        /// <summary>
        /// Customers this customer has blocked
        /// </summary>
        public IEnumerable<ICustomer> BlockedCustomers { get; }

        /// <summary>
        /// Add a photo the the custumer's photo list. 
        /// </summary>
        /// <param name="photo">The valid photo to add</param>
        IStatus<ICustomer> AddPhoto(IPhoto photo);

        /// <summary>
        /// Remove a photo the the custumer's photo list. 
        /// </summary>
        /// <param name="photo">The valid photo to remove</param>
        void RemovePhoto(IPhoto photo);

        /// <summary>
        /// Add an avatar to the avatars list. 
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns></returns>
        IStatus<ICustomer> AddAvatar(IAvatar avatar);

        /// <summary>
        /// Remove an avatar from the avatar list
        /// </summary>
        /// <param name="avatar"></param>
        void RemoveAvatar(IAvatar avatar);

        /// <summary>
        /// Block another customer. 
        /// </summary>
        /// <param name="customerToBlock"></param>
        /// <returns>Status indicating if block worked</returns>
        IStatus<ICustomer> BlockACustomer(ICustomer customerToBlock);

        IStatus<IEnumerable<string>> SelfDescriptions();
        IStatus<IEnumerable<IPhoto>> ImpressionPhotosOnly();
        IStatus<IEnumerable<IPhoto>> ExposingPhotosOnly();
    }
}
