using CSharpExtensions.Result;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserPassword
    {
        public string Value { get; }

        private UserPassword(string value)
        {
            Value = value;
        }

        public static Result<UserPassword> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<UserPassword>.Failure("User password is invalid");

            // add validation here
            return Result<UserPassword>.Success(new UserPassword(value));
        }
    }
}
