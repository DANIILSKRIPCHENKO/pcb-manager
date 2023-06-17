using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.UserNS;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.App
{
    public interface IUserAppService
    {
        public Task<Maybe<List<User>>> GetAllAsync();

        public Task<Result<User>> CreateAsync(CreateUserRequest createUserRequest);

        public Task<Maybe<User>> GetByIdAsync(UserId id);
    }
}
