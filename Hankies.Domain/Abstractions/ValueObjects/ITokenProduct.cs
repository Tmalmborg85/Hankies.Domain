namespace Hankies.Domain.Abstractions.ValueObjects
{
    public interface ITokenProduct 
    {
        public int TokenCost { get; }
        public string ProductName { get; }
    }
}