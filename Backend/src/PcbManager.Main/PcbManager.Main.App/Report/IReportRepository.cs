using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.App.Report;

public interface IReportRepository : IRepositoryBase<Domain.ReportNS.Report, ReportId>
{

}