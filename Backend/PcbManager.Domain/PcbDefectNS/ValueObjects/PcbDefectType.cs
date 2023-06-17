using CSharpExtensions.Result;

namespace PcbManager.Domain.PcbDefectNS.ValueObjects;

public class PcbDefectType
{
    public PcbDefectTypeEnum Value { get; }

    private PcbDefectType(PcbDefectTypeEnum value)
    {
        Value = value;
    }

    private PcbDefectType()
    {
    }

    public static Result<PcbDefectType> Create(PcbDefectTypeEnum value)
    {
        return Result<PcbDefectType>.Success(new PcbDefectType(value));
    }
}