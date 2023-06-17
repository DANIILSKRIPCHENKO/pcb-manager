using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.ReportNS;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.App;

public interface IReportRepository
{
    public Task<Result<Report>> CreateAsync(Image image);

    public Task<Maybe<Report>> GetByIdAsync(ReportId imageId);

    public Task<Maybe<List<Report>>> GetAllAsync();
}