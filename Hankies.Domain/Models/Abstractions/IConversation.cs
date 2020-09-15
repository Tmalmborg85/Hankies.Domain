using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
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


        IStatus<IConversation> SendMessage(IChatMessage message);

        IStatus<IConversation> Join(ICustomer customer);
        void Leave(ICustomer customer);
    }
}