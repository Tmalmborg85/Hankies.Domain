using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    public interface IVoteable<T>
    {
        /// <summary>
        /// Aggregate of up/down votes. 
        /// </summary>
        public IEnumerable<IVote> Votes { get; }

        /// <summary>
        /// Cast a distinct vote. 
        /// </summary>
        /// <param name="vote"></param>
        /// <returns>an instance of the votable object</returns>
        public IStatus<T> CastVote(IVote vote);
    }
}