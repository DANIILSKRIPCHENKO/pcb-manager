namespace PcbManager.Main.App.User;

public class UpdateUserRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
