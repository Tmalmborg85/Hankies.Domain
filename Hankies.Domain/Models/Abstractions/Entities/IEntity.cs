using System;

namespace Hankies.Domain.Models.Abstractions
{
    public interface IEntity
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
    }

    public interface IDeleteableEntity : IEntity
    {
        public DateTimeOffset DeletedAt { get; }
        public bool Deleted { get; set; }
    }
}