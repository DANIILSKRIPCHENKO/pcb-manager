using FluentAssertions;
using PcbManager.Main.Domain.UserNS;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.Tests;

public class UserTest
{
    [Fact]
    public void Create_ShouldReturnError_WhenAttemptingToCreateUserWithSameEmail()
    {
        var userOne = User.Create(
            UserName.Create("Ivan").Value,
            UserSurname.Create("Ivanov").Value,
            UserEmail.Create("test@gmail.com").Value,
            UserPassword.Create("12345").Value,
            new List<User>()
        );

        var userTwo = User.Create(
            UserName.Create("Dmitri").Value,
            UserSurname.Create("Dmitriev").Value,
            UserEmail.Create("test@gmail.com").Value,
            UserPassword.Create("12345").Value,
            new List<User>() { userOne.Value }
        );

        userTwo.IsFailure.Should().Be(true);
    }
}
