using CSharpExtensions.Result;

namespace PcbManager.Domain.ReportNS.ValueObjects;

public class ReportId
{
    public Guid Value { get; }

    private ReportId(Guid value)
    {
        Value = value;
    }

    public static Result<ReportId> Create(Guid value)
    {
        return Result<ReportId>.Success(new ReportId(value));
    }

    public static Result<ReportId> CreateUnique()
    {
        return Result<ReportId>.Success(new ReportId(Guid.NewGuid()));
    }
}