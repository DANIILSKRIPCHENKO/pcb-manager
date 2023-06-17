using CSharpExtensions.Result;
using System.Text.RegularExpressions;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserEmail
    {
        public string Value { get; }

        private UserEmail(string value)
        {
            Value = value;
        }

        public static Result<UserEmail> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<UserEmail>.Failure("User email must not be empty or white space");

            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = emailRegex.Match(value);
            if (!match.Success)
                return Result<UserEmail>.Failure("User email is invalid email");

            return Result<UserEmail>.Success(new UserEmail(value));
        }
    }
}
