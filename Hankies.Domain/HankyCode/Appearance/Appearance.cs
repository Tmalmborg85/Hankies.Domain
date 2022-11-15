using System;
namespace Hankies.Domain.HankyCode.Appearance
{
    /// <summary>
    /// Hankies can come in a varriaty of appearances but they all share some common ground. 
    /// </summary>
    public abstract class Appearance
    {

        public Appearance()
        {
        }

        public abstract string Description { get; }
    }
}
