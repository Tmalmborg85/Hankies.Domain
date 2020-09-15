using System;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// An distinct entity that can up or down vote once. 
    /// </summary>
    public interface IVoter
    {
        /// <summary>
        /// Unique ID of a voting entity
        /// </summary>
        public Guid VoterID { get; }

        /// <summary>
        /// Submits a vote from this entity to a voteable thing
        /// </summary>
        /// <param name="voteableThing">The thing that can recive this entities
        /// vote</param>
        public void UpVote(IVoteable voteableThing);

        /// <inheritdoc cref="UpVote(IVoteable)"/>
        public void DownVote(IVoteable votableThing);

    }
}