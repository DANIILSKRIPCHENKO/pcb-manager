using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.ImageNS.ValueObjects;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.App.Report;

public class ReportAppService : IReportAppService
{
    private readonly IReportRepository _reportRepository;

    public ReportAppService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<List<Domain.ReportNS.Report>, BaseError>> GetAllAsync() =>
        await _reportRepository.GetAllAsync();

    public async Task<Result<Domain.ReportNS.Report, BaseError>> GetByIdAsync(ReportId id) =>
        await _reportRepository.GetByIdAsync(id);

    public async Task<Result<Domain.ReportNS.Report, BaseError>> CreateAsync(CreateReportRequest createReportRequest) =>
        await ImageId.Create(createReportRequest.ImageId)
            .Bind(Domain.ReportNS.Report.Create)
            .Bind(report => _reportRepository.CreateAsync(report));

    public async Task<Result<Domain.ReportNS.Report, BaseError>> DeleteAsync(ReportId id) =>
        await _reportRepository.GetByIdAsync(id).Bind(report => _reportRepository.DeleteAsync(report));
}