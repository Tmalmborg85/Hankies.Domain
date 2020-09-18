using System;
namespace Hankies.Domain.Abstractions.DomainEvents
{
    /// <summary>
    /// Base abstraction for Hankies domain events. 
    /// </summary>
    /// <remarks>
    /// Its not unexpected for this to be empty or near empty. Domain events
    /// are really just DTOs. This interface is just to mark them as a type
    /// </remarks>
    public interface IDomainEvent
    {
    }
}
