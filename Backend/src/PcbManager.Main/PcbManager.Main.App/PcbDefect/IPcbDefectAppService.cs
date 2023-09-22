using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.App.PcbDefect;

public interface IPcbDefectAppService
{
    public Task<Result<List<Domain.PcbDefectNS.PcbDefect>, BaseError>> GetAllAsync();

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> GetByIdAsync(PcbDefectId id);

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> CreateAsync(
        CreatePcbDefectRequest createPcbDefectRequest
    );

    public Task<Result<Domain.PcbDefectNS.PcbDefect, BaseError>> DeleteAsync(PcbDefectId id);
}
