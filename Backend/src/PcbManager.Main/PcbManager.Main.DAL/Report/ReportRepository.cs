using PcbManager.Main.App.Report;
using PcbManager.Main.DAL.Abstractions;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.DAL.Report;

public class ReportRepository : RepositoryBase<Domain.ReportNS.Report, ReportId>, IReportRepository
{
    public ReportRepository(PcbManagerDbContext context) : base(context)
    {

    }
}