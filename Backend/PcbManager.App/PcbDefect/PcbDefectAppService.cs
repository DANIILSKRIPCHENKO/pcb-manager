using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.PcbDefectNS.ValueObjects;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.App.PcbDefect;

public class PcbDefectAppService : IPcbDefectAppService
{
    private readonly IPcbDefectRepository _repository;

    public PcbDefectAppService(IPcbDefectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<Domain.PcbDefectNS.PcbDefect>, BaseError>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> GetByIdAsync(PcbDefectId id) =>
        await _repository.GetByIdAsync(id);

    public async Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> CreateAsync(
        CreatePcbDefectRequest createPcbDefectRequest)
    {
        var pcbDefectType = PcbDefectType.Create(createPcbDefectRequest.PcbDefectTypeEnum);
        if (pcbDefectType.IsFailure)
            return pcbDefectType.Error;

        var reportId = ReportId.Create(createPcbDefectRequest.ReportId);
        if (reportId.IsFailure)
            return reportId.Error;

        return await Domain.PcbDefectNS.PcbDefect.Create(pcbDefectType.Value, reportId.Value)
            .Bind(pcbDefect => _repository.CreateAsync(pcbDefect));
    }

    public async Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> DeleteAsync(PcbDefectId id) =>
        await _repository.GetByIdAsync(id).Bind(pcbDefect => _repository.DeleteAsync(pcbDefect));
}