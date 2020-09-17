using System.Collections.Generic;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    public interface IHandkerchief : IDomainEntity, ICommunityTips
    {
        #region Properties

        /// <summary>
        /// What material is this handkerchief made from.
        /// </summary>
        public MaterialTypes Material { get; }

        /// <summary>
        /// The named color hex value to use as a base color.
        /// </summary>
        public INamedColor Color { get; }

        /// <summary>
        /// A pattern, such as red stripe or checker.
        /// </summary>
        public IPattern Pattern { get; }

        /// <summary>
        /// A one to four word title for this handkerchief.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// An breif overview of what this Handkerchief is all about.
        /// </summary>
        public string Overview { get; }

        /// <summary>
        /// What this handkerchief is currently being flagged as.
        /// </summary>
        public IFlaggingAs Flagging { get; }

        /// <summary>
        /// What pockets this handkercheif is currently being displayed/flagged
        /// in 
        /// </summary>
        public IEnumerable<PocketTypes> InPockets { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Display this handkerchief in a pocket if its not already being
        /// displayed. 
        /// </summary>
        /// <param name="pocket">pocket to flag in</param>
        void FlagInPockect(PocketTypes pocket);

        /// <summary>
        /// Cease to display this handkerchief in a pocket if its being
        /// displayed.
        /// </summary>
        /// <param name="pocket">pocket to flag in</param>
        void StopFlaggingInPockect(PocketTypes pocket);

        /// <summary>
        /// Update this handkerchief with a new Title
        /// </summary>
        /// <param name="newTitle"></param>
        /// <returns></returns>
        IStatus<IHandkerchief> UpdateTitle(string newTitle);

        /// <summary>
        /// Update this handkerchief with a new overview
        /// </summary>
        /// <param name="newOverview"></param>
        /// <returns></returns>
        IStatus<IHandkerchief> UpdateOverview(string newOverview);

        #endregion
    }

}
