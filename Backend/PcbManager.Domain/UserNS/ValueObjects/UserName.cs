using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserName
    {
        public string Value { get; }

        private UserName(string value)
        {
            Value = value;
        }

        public static Result<UserName, ValidationError> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<UserName, ValidationError>(new ValidationError());

            // add validation here
            return Result.Success<UserName, ValidationError>(new UserName(value));
        }
    }
}
