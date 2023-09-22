using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.App.User
{
    public interface IUserAppService
    {
        public Task<Result<List<Domain.UserNS.User>, BaseError>> GetAllAsync();

        public Task<Result<Domain.UserNS.User, BaseError>> CreateAsync(
            CreateUserRequest createUserRequest
        );

        public Task<Result<Domain.UserNS.User, BaseError>> GetByIdAsync(Guid id);

        public Task<Result<Domain.UserNS.User, BaseError>> DeleteAsync(Guid id);

        public Task<Result<Domain.UserNS.User, BaseError>> UpdateAsync(
            Guid id,
            UpdateUserRequest updateUserRequest
        );
    }
}
