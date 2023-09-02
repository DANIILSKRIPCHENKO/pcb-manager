using PcbManager.App.Abstractions;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.App.Report;

public interface IReportRepository : IRepositoryBase<Domain.ReportNS.Report, ReportId>
{

}