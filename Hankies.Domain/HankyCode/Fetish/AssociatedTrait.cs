using System;
using System.Collections.Generic;

namespace Hankies.Domain.HankyCode.Fetish
{
    /// <summary>
    /// Associated Traits are traits that can be associated with an unworn flag. Traits can be what people are, or are into. Each trait must have a <paramref name="name"/>, that is one to two words, and list of <paramref name="rolls"/>. 
    /// <remarks>
    /// </remarks>
    /// <example>
    /// For Example:
    /// <code>
    /// new AssociatedTrait("Sadomasochism", Rolls.TopBottomRolls);
    /// </code>
    /// </example>
    /// </summary>
	public class AssociatedTrait
	{
        /// <summary>
        /// Create a new <c>Associated Trait</c> with a name and flaggable rolls only. Not all rolls will have a definition or history
        /// </summary>
        /// <param name="rolls">the new x-coordinate.</param>
        /// <param name="name">the 1-2 word name of the new trait.</param>
        public AssociatedTrait(string name, List<FlaggedRoll> rolls)
        {
            Name = name;
            Rolls = rolls;
        }

        public AssociatedTrait(string name, List<FlaggedRoll> rolls, string definition) : this(name, rolls)
        {
            Definition = definition;
        }

        public AssociatedTrait(string name, List<FlaggedRoll> rolls, string definition, string history) : this(name, rolls, definition)
        {
            History = history;
        }

        public string Name { get; set; }
        public string Definition { get; set; }
        public string History { get; }
        public List<FlaggedRoll> Rolls { get; set; }
    }
}

