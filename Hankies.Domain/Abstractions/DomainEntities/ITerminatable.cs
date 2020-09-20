using System;
namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface ITerminatable
    {
        public DateTimeOffset TerminatedAt { get; }

        public void Terminate();
    }
}
