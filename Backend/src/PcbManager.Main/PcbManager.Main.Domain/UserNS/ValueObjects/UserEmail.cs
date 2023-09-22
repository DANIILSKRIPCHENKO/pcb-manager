using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors;

namespace PcbManager.Main.Domain.UserNS.ValueObjects
{
    public class UserEmail
    {
        public string Value { get; }

        private UserEmail(string value)
        {
            Value = value;
        }

        public static Result<UserEmail, ValidationError> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<UserEmail, ValidationError>(new ValidationError());

            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = emailRegex.Match(value);
            if (!match.Success)
                return Result.Failure<UserEmail, ValidationError>(new ValidationError());

            return Result.Success<UserEmail, ValidationError>(new UserEmail(value));
        }
    }
}
