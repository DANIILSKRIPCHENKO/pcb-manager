using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.Common;

public class UpdatedAt
{
    public DateTime Value { get; }

    private UpdatedAt(DateTime value)
    {
        Value = value;
    }

    public static Result<UpdatedAt, BaseError> FromNow() =>
        Result.Success<UpdatedAt, BaseError>(new UpdatedAt(DateTime.Now));

    public static Result<UpdatedAt, BaseError> Create(DateTime dateTime) =>
        Result.Success<UpdatedAt, BaseError>(new UpdatedAt(dateTime));
}