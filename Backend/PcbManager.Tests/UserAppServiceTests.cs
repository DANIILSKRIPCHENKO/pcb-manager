using CSharpExtensions.Maybe;
using Moq;
using PcbManager.App;
using PcbManager.Domain.UserNS;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.Tests;

public class UserAppServiceTests
{
    [Fact]
    public void GetByIdAsync_ShouldGetData_FromRepository()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var userToReturn = User.Create(
            UserName.Create("Ivan").Value,
            UserSurname.Create("Ivanov").Value,
            UserEmail.Create("test@gmail.com").Value,
            UserPassword.Create("12345").Value,
            new List<User>());

        userRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<UserId>()))
            .ReturnsAsync(Maybe<User>.From(userToReturn.Value));

        var userAppService = new UserAppService(userRepositoryMock.Object);

        var user = userAppService.GetByIdAsync(UserId.CreateUnique().Value);

        userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<UserId>()), Times.Once);
    }
}