using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.App.Report;

public interface IReportAppService
{
    public Task<Result<List<Domain.ReportNS.Report>, BaseError>> GetAllAsync();

    public Task<Result<Domain.ReportNS.Report, BaseError>> GetByIdAsync(ReportId id);

    public Task<Result<Domain.ReportNS.Report, BaseError>> CreateAsync(CreateReportRequest createReportRequest);

    public Task<Result<Domain.ReportNS.Report, BaseError>> DeleteAsync(ReportId id);
}