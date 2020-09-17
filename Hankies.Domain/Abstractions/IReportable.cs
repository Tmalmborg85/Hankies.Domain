using System.Collections.Generic;

namespace Hankies.Domain.Models.Abstractions
{
    /// <summary>
    /// An entity that can be reported for violations by community members
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReportableContent
    {
        /// <summary>
        /// Reports about this entity. 
        /// </summary>
        public IEnumerable<ICommunityReport> Reports { get; }

        /// <summary>
        /// Add a new community report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        IStatus<IReportableContent> AddNewReport(ICommunityReport report);
    }
}