using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.App.Report;

public interface IReportAppService
{
    public Task<Result<List<Domain.ReportNS.Report>, BaseError>> GetAllAsync();

    public Task<Result<Domain.ReportNS.Report, BaseError>> GetByIdAsync(ReportId id);

    public Task<Result<Domain.ReportNS.Report, BaseError>> CreateAsync(
        CreateReportRequest createReportRequest
    );

    public Task<Result<Domain.ReportNS.Report, BaseError>> DeleteAsync(ReportId id);
}
