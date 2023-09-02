using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.Common;

public class CreatedAt
{
    public DateTime Value { get; }

    private CreatedAt(DateTime value)
    {
        Value = value;
    }

    public static Result<CreatedAt, BaseError> FromNow() =>
        Result.Success<CreatedAt, BaseError>(new CreatedAt(DateTime.Now));

    public static Result<CreatedAt, BaseError> Create(DateTime dateTime) =>
        Result.Success<CreatedAt, BaseError>(new CreatedAt(dateTime));
}