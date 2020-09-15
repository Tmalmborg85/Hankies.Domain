using System;
namespace Hankies.Domain.Models.Abstractions
{
    public enum VoteOptions
    {
        Up, Down
    }

    /// <summary>
    /// An updown vote by a distinct customer
    /// </summary>
    public interface IVote
    {
        /// <summary>
        /// The distinct customer casting this vote.
        /// </summary>
        public IVoter Voter { get; }

        /// <summary>
        /// The up/down vote cast
        /// </summary>
        public VoteOptions Vote { get; }
    }
}
