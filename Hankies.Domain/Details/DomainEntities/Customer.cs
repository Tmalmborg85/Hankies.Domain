using System;
using System.Collections.Generic;
using System.Linq;
using Hankies.Domain.Abstractions.DomainEntities;
using Hankies.Domain.HelperClasses;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Details.DomainEntities
{
    /// <summary>
    /// A distinct person using Hankies as a customer. 
    /// </summary>
    public class Customer : DomainEntity, IDeletableDomainEntity, IVoter
    {
        #region Properties

        /// <summary>
        /// Distict ID to use when chatting.
        /// </summary>
        public Guid ChatID { get; private set; }

        /// <summary>
        /// Distict ID to use when blocking this Customer.
        /// </summary>
        public Guid BlockedID { get; private set; }

        /// <summary>
        /// All the photos this customer owns.
        /// </summary>
        public IEnumerable<IPhoto> Photos { get; private set; }

        /// <summary>
        /// All the avatars this customer has created.
        /// </summary>
        public IEnumerable<Avatar> Avatars { get; private set; }

        /// <summary>
        /// Hashtable backing to blocked customers.
        /// </summary>
        /// <remarks>
        /// Lookups in blocked customer will be many. using a dictinary means
        /// the lookup time in O(1) not O(N). It is worth the cost to build a
        /// dicinary from database values list in this case. 
        /// </remarks>
        private Dictionary<Guid, Customer> _blockedCustomers { get; }

        /// <summary>
        /// Customers this customer has blocked
        /// </summary>
        public IEnumerable<Customer> BlockedCustomers => _blockedCustomers?
            .Values;

        public Guid VoterID { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public bool Deleted { get; private set; }

        #endregion

        /// <summary>
        /// Does a lookp to see if the customer is in this customers blocked
        /// list. 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool HasBlocked(Customer customer)
        {
            return _blockedCustomers.ContainsKey(customer.BlockedID);
        }

        /// <summary>
        /// Add a photo to the custumer's photo list. 
        /// </summary>
        /// <param name="photo">The valid photo to add</param>
        IStatus<Customer> AddPhoto(IPhoto photo)
        {

        }

        /// <summary>
        /// Remove a photo the the custumer's photo list. 
        /// </summary>
        /// <param name="photo">The valid photo to remove</param>
        void RemovePhoto(IPhoto photo)
        {

        }

        /// <summary>
        /// Add an avatar to the avatars list. 
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns></returns>
        IStatus<Customer> AddAvatar(IAvatar avatar)
        {

        }

        /// <summary>
        /// Remove an avatar from the avatar list
        /// </summary>
        /// <param name="avatar"></param>
        void RemoveAvatar(IAvatar avatar)
        {

        }

        /// <summary>
        /// Block another customer. 
        /// </summary>
        /// <param name="customerToBlock"></param>
        /// <remarks>
        /// There is not a way to unblock a customer</remarks>
        /// <returns>Status indicating if block worked</returns>
        IStatus<Customer> BlockACustomer(Customer customerToBlock)
        {

        }

        /// <summary>
        /// Get all the first impression descriptions this customer has written
        /// </summary>
        /// <returns></returns>
        IStatus<IEnumerable<string>> SelfDescriptions()
        {

        }

        /// <summary>
        /// Get all the SFW photos
        /// </summary>
        /// <returns></returns>
        IStatus<IEnumerable<IPhoto>> SFWPhotosOnly()
        {

        }

        /// <summary>
        /// Gets all teh NSFW photos. 
        /// </summary>
        /// <returns></returns>
        IStatus<IEnumerable<IPhoto>> NSFWPhotosOnly()
        {

        }

        public void UpVote(IVoteable<object> voteableThing)
        {
            throw new NotImplementedException();
        }

        public void DownVote(IVoteable<object> votableThing)
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
    }
}
