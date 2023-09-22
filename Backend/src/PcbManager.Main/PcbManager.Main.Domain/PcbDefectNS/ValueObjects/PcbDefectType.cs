using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

public class PcbDefectType
{
    public PcbDefectTypeEnum Value { get; }

    private PcbDefectType(PcbDefectTypeEnum value)
    {
        Value = value;
    }

    private PcbDefectType() { }

    public static Result<PcbDefectType, BaseError> Create(PcbDefectTypeEnum value) =>
        Result.Success<PcbDefectType, BaseError>(new PcbDefectType(value));
}
