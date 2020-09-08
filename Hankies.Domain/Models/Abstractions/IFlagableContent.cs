using System;
namespace Hankies.Domain.Models.Abstractions
{
    
    /// <summary>
    /// Abstraction for objects which can be flagged for violating comunity
    /// standards
    /// </summary>
    public interface IFlagableContent
    {
        /// <summary>
        /// Indicated if this content has been flagged by a user
        /// </summary>
        public bool IsFlagged { get; }
        public FlaggedCodes FlaggedReason { get; }
        public ICustomer FlaggedBy { get; }
        public string FlaggedMessage { get; }


        void FlagContent(FlaggedCodes reason, ICustomer flaggedBy);
        void FlagContent(FlaggedCodes reason, ICustomer flaggedBy, string message);
    }
}
