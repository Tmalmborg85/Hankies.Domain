using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A holder of messages and members
    /// </summary>
    public interface IConversation : IEntity
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
        IStatus<IConversation> Join(ICustomer customer);

        /// <summary>
        /// Remove a customer's chat ID from this conversation. 
        /// </summary>
        /// <param name="customer"></param>
        void Leave(ICustomer customer);
    }
}