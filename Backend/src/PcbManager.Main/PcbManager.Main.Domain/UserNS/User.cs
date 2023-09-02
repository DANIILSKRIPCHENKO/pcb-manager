using CSharpFunctionalExtensions;
using PcbManager.Main.Domain.Abstractions;
using PcbManager.Main.Domain.Common;
using PcbManager.Main.Domain.Errors;
using PcbManager.Main.Domain.Errors.Abstractions;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.Domain.UserNS
{
    public class User : IIdEntity<UserId>, ICreatedAtEntity, IUpdatedAtEntity
    {
        private User(UserName name, UserSurname surname, UserEmail email, UserPassword password)
        {
            Id = UserId.CreateUnique();
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            CreatedAt = CreatedAt.FromNow().Value;
            UpdatedAt = UpdatedAt.FromNow().Value;
        }

        public UserId Id { get; }

        public UserName Name { get; private set; }

        public UserSurname Surname { get; private set; }

        public UserEmail Email { get; private set; }

        public UserPassword Password { get; private set; }

        public CreatedAt CreatedAt { get; }

        public UpdatedAt UpdatedAt { get; private set; }

        public static Result<User, ConflictError> Create(UserName name, UserSurname surname, UserEmail email, UserPassword password, IEnumerable<User> users)
        {
            // починить сравнение
            if (users.Any(x => x.Email.Value == email.Value))
                return Result.Failure<User, ConflictError>(new ConflictError());

            return Result.Success<User, ConflictError>(new User(name, surname, email, password));
        }

        public Result<User, BaseError> Update(UserName name, UserSurname surname, UserEmail email,
            UserPassword password, IEnumerable<User> users)
        {
            if (users.Any(x => x.Email.Value == email.Value))
                return Result.Failure<User, BaseError>(new ConflictError());

            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            UpdatedAt = UpdatedAt.FromNow().Value;

            return Result.Success<User, BaseError>(this);
        }
    }
}
