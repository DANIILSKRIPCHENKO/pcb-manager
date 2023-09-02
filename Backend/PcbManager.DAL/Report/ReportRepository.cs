using PcbManager.App.Report;
using PcbManager.DAL.Abstractions;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.DAL.Report;

public class ReportRepository : RepositoryBase<Domain.ReportNS.Report, ReportId>, IReportRepository
{
    public ReportRepository(PcbManagerDbContext context) : base(context)
    {

    }
}