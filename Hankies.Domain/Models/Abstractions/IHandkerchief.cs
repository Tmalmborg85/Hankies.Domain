using System;
using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    public interface IHandkerchief : ICommunityTips
    {
        /// <summary>
        /// What material is this handkerchief made from
        /// </summary>
        public MaterialTypes Material { get; }

        /// <summary>
        /// The named color hex value to use as a base color
        /// </summary>
        public INamedColor Color { get; }

        /// <summary>
        /// A pattern, such as red stripe or checker
        /// </summary>
        public IPattern Pattern { get; }

        /// <summary>
        /// A one to four word title for this handkerchief
        /// </summary>
        public string Title { get; }
        public string Overview { get; }
        public IFlaggingAs Flagging { get; }

        public IEnumerable<PocketTypes> InPockets { get; }

        void FlagInPockect(PocketTypes pocket);
        void StopFlaggingInPockect(PocketTypes pocket);
    }

}
