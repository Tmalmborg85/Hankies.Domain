using System;
using Hankies.Domain.Models.Abstractions;

namespace Hankies.Domain.Abstractions.DomainEntities
{
    /// <summary>
    /// A report that can be filed on reportable content
    /// </summary>
    public interface ICommunityReport
    {
        #region Properties
        /// <summary>
        /// When this report was craeted
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Who is fileing this report.
        /// </summary>
        public  CCustomer Reporter { get; }

        /// <summary>
        /// The reason code for this report
        /// </summary>
        public ReportReasons Reason { get; }

        /// <summary>
        /// a customer specified reason for this report.  
        /// </summary>
        public string OtherReason { get; }

        /// <summary>
        /// An option customer entered message to acompany this report. 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The content that is being reported
        /// </summary>
        public IReportableContent Content { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Action to file this report. 
        /// </summary>
        /// <returns></returns>
        IStatus<ICommunityReport> FileReport();

        #endregion
    }
}