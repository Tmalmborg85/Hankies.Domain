using System.Collections.Generic;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// An entity that customers can vote on. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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