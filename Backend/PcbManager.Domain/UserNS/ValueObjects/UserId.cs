using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.Domain.UserNS.ValueObjects
{
    public class UserId
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static Result<UserId> Create(Guid value)
        {
            return Result<UserId>.Success(new UserId(value));
        }

        public static Result<UserId> CreateUnique()
        {
            return Result<UserId>.Success(new UserId(Guid.NewGuid()));
        }
    }
}
