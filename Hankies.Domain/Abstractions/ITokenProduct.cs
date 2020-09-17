namespace Hankies.Domain.Models.Abstractions
{
    public interface ITokenProduct 
    {
        public int TokenCost { get; }
        public string ProductName { get; }
    }
}