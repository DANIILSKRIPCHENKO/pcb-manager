
using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.PcbDefectNS.ValueObjects;

public class PcbDefectId
{
    public Guid Value { get; }

    private PcbDefectId(Guid value)
    {
        Value = value;
    }

    public static Result<PcbDefectId, BaseError> Create(Guid value) =>
        Result.Success<PcbDefectId, BaseError>(new PcbDefectId(value));

    public static Result<PcbDefectId, BaseError> CreateUnique() =>
        Result.Success<PcbDefectId, BaseError>(new PcbDefectId(Guid.NewGuid()));

}