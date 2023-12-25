using CSharpFunctionalExtensions;
using PcbManager.Main.App.Image;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.App.User
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionManager _transactionManager;
        private readonly IImageFileSystem _imageFileSystem;

        public UserAppService(
            IUserRepository userRepository,
            ITransactionManager transactionManager,
            IImageFileSystem imageFileSystem
        )
        {
            _userRepository = userRepository;
            _transactionManager = transactionManager;
            _imageFileSystem = imageFileSystem;
        }

        public async Task<Result<List<Domain.UserNS.User>, BaseError>> GetAllAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<Result<Domain.UserNS.User, BaseError>> CreateAsync(
            CreateUserRequest createUserRequest
        )
        {
            var userNameResult = UserName.Create(createUserRequest.Name);
            if (userNameResult.IsFailure)
                return userNameResult.Error;

            var userSurnameResult = UserSurname.Create(createUserRequest.Surname);
            if (userSurnameResult.IsFailure)
                return userSurnameResult.Error;

            var userEmailResult = UserEmail.Create(createUserRequest.Email);
            if (userEmailResult.IsFailure)
                return userEmailResult.Error;

            var userPasswordResult = UserPassword.Create(createUserRequest.Password);
            if (userPasswordResult.IsFailure)
                return userPasswordResult.Error;

            var usersResult = await _userRepository.GetAllAsync();
            if (usersResult.IsFailure)
                return usersResult.Error;

            var userResult = Domain.UserNS.User.Create(
                userNameResult.Value,
                userSurnameResult.Value,
                userEmailResult.Value,
                userPasswordResult.Value,
                usersResult.Value
            );

            if (userResult.IsFailure)
                return userResult.Error;

            return await _transactionManager.ExecuteInTransactionAsync(
                async () =>
                    await _userRepository
                        .CreateAsync(userResult.Value)
                        .Tap(user => _imageFileSystem.CreateFolder(user.Id))
            );
        }

        public async Task<Result<Domain.UserNS.User, BaseError>> GetByIdAsync(Guid id) =>
            await _userRepository.GetByIdAsync(UserId.Create(id));

        public async Task<Result<Domain.UserNS.User, BaseError>> DeleteAsync(Guid id) =>
            await _userRepository
                .GetByIdAsync(UserId.Create(id))
                .Bind(
                    async user =>
                        await _transactionManager.ExecuteInTransactionAsync(
                            async () =>
                                await _userRepository
                                    .DeleteAsync(user)
                                    .Tap(user => _imageFileSystem.DeleteFolder(user.Id))
                        )
                );

        public async Task<Result<Domain.UserNS.User, BaseError>> UpdateAsync(
            Guid id,
            UpdateUserRequest updateUserRequest
        )
        {
            var userNameResult = UserName.Create(updateUserRequest.Name);
            if (userNameResult.IsFailure)
                return userNameResult.Error;

            var userSurnameResult = UserSurname.Create(updateUserRequest.Surname);
            if (userSurnameResult.IsFailure)
                return userSurnameResult.Error;

            var userEmailResult = UserEmail.Create(updateUserRequest.Email);
            if (userEmailResult.IsFailure)
                return userEmailResult.Error;

            var userPasswordResult = UserPassword.Create(updateUserRequest.Password);
            if (userPasswordResult.IsFailure)
                return userPasswordResult.Error;

            var usersResult = await _userRepository.GetAllAsync();
            if (usersResult.IsFailure)
                return usersResult.Error;

            return await _userRepository
                .GetByIdAsync(UserId.Create(id))
                .Bind(
                    user =>
                        user.Update(
                            userNameResult.Value,
                            userSurnameResult.Value,
                            userEmailResult.Value,
                            userPasswordResult.Value,
                            usersResult.Value
                        )
                )
                .Bind(user => _userRepository.UpdateAsync(user));
        }
    }
}
