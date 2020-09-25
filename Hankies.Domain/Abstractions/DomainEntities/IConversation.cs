using System;
using System.Collections.Generic;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A holder of messages and members
    /// </summary>
    public interface IConversation : IDomainEntity
    {
        /// <summary>
        /// The Chat IDs of who is in this conversation.
        /// </summary>
        public IEnumerable<Guid> MemberChatIDs { get; }

        /// <summary>
        /// The messages that belong to this conversation
        /// </summary>
        public IEnumerable<IChatMessage> Messages { get; }

        /// <summary>
        /// Adds a message to messages. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IStatus<IConversation> SendMessage(IChatMessage message);

        /// <summary>
        /// Adds a new Customer's chat ID to thsi conversation. 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        IStatus<IConversation> Join((CCustomer customer);

        /// <summary>
        /// Remove a customer's chat ID from this conversation. 
        /// </summary>
        /// <param name="customer"></param>
        void Leave((CCustomer customer);
    }
}