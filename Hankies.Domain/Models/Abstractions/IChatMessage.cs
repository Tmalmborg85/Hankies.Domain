using System;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// A chat message.
    /// </summary>
    public interface IChatMessage : IReportableContent, IEntity
    {
        /// <summary>
        /// When this message was sent by a service.
        /// </summary>
        public DateTimeOffset SentAt { get; }

        /// <summary>
        /// When a recipient read the message
        /// </summary>
        public DateTimeOffset ReadAt { get; }

        /// <summary>
        /// Chat ID of the sender
        /// </summary>
        public Guid SenderChatID { get; }

        /// <summary>
        /// Conversation ID this messahe belongs to
        /// </summary>
        public Guid ConversationID { get; }

        /// <summary>
        /// The text of this message. 
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Handles seting the message to sent after it has been sent.
        /// </summary>
        void Sent();

        /// <summary>
        /// Sets the 
        /// </summary>
        void Read();
    }
}