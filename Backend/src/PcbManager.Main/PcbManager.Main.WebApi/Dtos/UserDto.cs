using PcbManager.Main.Domain.UserNS;

namespace PcbManager.Main.WebApi.Dtos;

public class UserDto
{
    private UserDto(User user)
    {
        Id = user.Id.Value;
        Name = user.Name.Value;
        Surname = user.Surname.Value;
        Email = user.Email.Value;
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Surname { get; }

    public string Email { get; }

    public static UserDto From(User user) => new(user);
}
