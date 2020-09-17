using System;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A chat message.
    /// </summary>
    public interface IChatMessage : IDomainEntity, IReportableContent
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
        /// The text of this message. 
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Handles seting the message to sent after it has been sent.
        /// </summary>
        void Sent(DateTimeOffset sentAt);

        /// <summary>
        /// Handles seting this message to read after it has been read by a
        /// recipient.
        /// </summary>
        void Read(DateTimeOffset readAt);
    }
}