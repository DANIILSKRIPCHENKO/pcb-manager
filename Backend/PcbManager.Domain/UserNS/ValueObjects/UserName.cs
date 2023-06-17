using CSharpExtensions.Result;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserName
    {
        public string Value { get; }

        private UserName(string value)
        {
            Value = value;
        }

        public static Result<UserName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<UserName>.Failure("User name is invalid");

            // add validation here
            return Result<UserName>.Success(new UserName(value));
        }
    }
}
