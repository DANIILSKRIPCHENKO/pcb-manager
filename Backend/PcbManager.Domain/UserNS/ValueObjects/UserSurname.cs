using CSharpExtensions.Result;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserSurname
    {
        public string Value { get; }

        private UserSurname(string value)
        {
            Value = value;
        }

        public static Result<UserSurname> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<UserSurname>.Failure("User surname is invalid");

            // add validation here
            return Result<UserSurname>.Success(new UserSurname(value));
        }
    }
}
