using CSharpExtensions.Result;
using PcbManager.Domain.ImageNS;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.Domain.UserNS
{
    public class User
    {
        private readonly List<Image> _images = new();

        private User(UserName name, UserSurname surname, UserEmail email, UserPassword password)
        {
            Id = UserId.CreateUnique().Value;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
        }

        public UserId Id { get; }

        public UserName Name { get; }

        public UserSurname Surname { get; }

        public UserEmail Email { get; }

        public UserPassword Password { get; }

        public IReadOnlyList<Image> Images => _images.AsReadOnly();


        public static Result<User> Create(UserName name, UserSurname surname, UserEmail email, UserPassword password, List<User> users)
        {
            // починить сравнение
            if(users.Any(x=> x.Email.Value == email.Value))
                return Result<User>.Failure("User email should be unique");

            return Result<User>.Success(new User(name, surname, email, password));
        }
    }
}
