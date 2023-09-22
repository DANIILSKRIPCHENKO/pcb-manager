using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.Domain.ReportNS.ValueObjects;

public class ReportId
{
    public Guid Value { get; }

    private ReportId(Guid value)
    {
        Value = value;
    }

    public static Result<ReportId, BaseError> Create(Guid value)
    {
        return Result.Success<ReportId, BaseError>(new ReportId(value));
    }

    public static Result<ReportId, BaseError> CreateUnique()
    {
        return Result.Success<ReportId, BaseError>(new ReportId(Guid.NewGuid()));
    }
}
