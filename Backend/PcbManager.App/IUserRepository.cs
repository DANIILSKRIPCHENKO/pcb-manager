using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.UserNS;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.App
{
    public interface IUserRepository
    {
        public Task<Maybe<List<User>>> GetAllAsync();

        public Task<Result<User>> CreateAsync(User user);

        public Task<Maybe<User>> GetByIdAsync(UserId id);
    }
}
