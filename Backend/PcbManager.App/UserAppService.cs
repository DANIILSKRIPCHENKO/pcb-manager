using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using PcbManager.Domain.UserNS;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.App
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> CreateAsync(CreateUserRequest createUserRequest)
        {
            var userNameResult = UserName.Create(createUserRequest.Name);
            if (userNameResult.IsFailure)
                return Result<User>.Failure(userNameResult.Error);

            var userSernameResult = UserSurname.Create(createUserRequest.Surname);
            if (userSernameResult.IsFailure)
                return Result<User>.Failure(userSernameResult.Error);

            var userEmailResult = UserEmail.Create(createUserRequest.Email);
            if (userEmailResult.IsFailure)
                return Result<User>.Failure(userEmailResult.Error);

            var userPasswordResult = UserPassword.Create(createUserRequest.Password);
            if (userPasswordResult.IsFailure)
                return Result<User>.Failure(userPasswordResult.Error);

            var maybeUsers = await _userRepository.GetAllAsync();

            var userResult = User.Create(
                userNameResult.Value,
                userSernameResult.Value,
                userEmailResult.Value,
                userPasswordResult.Value,
                maybeUsers.HasValue ? maybeUsers.Value : new List<User>());

            if (userResult.IsFailure)
                return Result<User>.Failure(userResult.Error);

            return await _userRepository.CreateAsync(userResult.Value);
        }

        public async Task<Maybe<List<User>>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Maybe<User>> GetByIdAsync(UserId id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}
