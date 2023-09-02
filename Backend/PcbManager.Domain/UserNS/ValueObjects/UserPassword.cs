using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserPassword
    {
        public string Value { get; }

        private UserPassword(string value)
        {
            Value = value;
        }

        public static Result<UserPassword, ValidationError> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<UserPassword, ValidationError>(new ValidationError());

            // add validation here
            return Result.Success<UserPassword, ValidationError>(new UserPassword(value));
        }
    }
}
