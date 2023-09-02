using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;
using PcbManager.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.App.PcbDefect;

public interface IPcbDefectAppService
{
    public Task<Result<List<Domain.PcbDefectNS.PcbDefect>, BaseError>> GetAllAsync();

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> GetByIdAsync(PcbDefectId id);

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> CreateAsync(CreatePcbDefectRequest createPcbDefectRequest);

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> DeleteAsync(PcbDefectId id);
}