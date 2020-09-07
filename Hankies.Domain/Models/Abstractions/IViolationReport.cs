using System;
namespace Hankies.Domain.Models.Abstractions
{
    public enum ReportReasons
    {

    }

    /// <summary>
    /// An object to report content of possible bad behavior
    /// </summary>
    public interface IViolationReport
    {
        public ICustomer Reporter { get; }
        public ReportReasons Reason { get; }
        public string Message { get; }
    }
}
