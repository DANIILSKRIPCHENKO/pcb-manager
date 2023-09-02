using CSharpFunctionalExtensions;
using PcbManager.Domain.Errors;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserSurname
    {
        public string Value { get; }

        private UserSurname(string value)
        {
            Value = value;
        }

        public static Result<UserSurname, ValidationError> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<UserSurname, ValidationError>(new ValidationError());

            // add validation here
            return Result.Success<UserSurname, ValidationError>(new UserSurname(value));
        }
    }
}
