using CSharpExtensions.Result;

namespace PcbManager.Domain.PcbDefectNS.ValueObjects;

public class PcbDefectId
{
    public Guid Value { get; }

    private PcbDefectId(Guid value)
    {
        Value = value;
    }

    public static Result<PcbDefectId> Create(Guid value)
    {
        return Result<PcbDefectId>.Success(new PcbDefectId(value));
    }

    public static Result<PcbDefectId> CreateUnique()
    {
        return Result<PcbDefectId>.Success(new PcbDefectId(Guid.NewGuid()));
    }
}