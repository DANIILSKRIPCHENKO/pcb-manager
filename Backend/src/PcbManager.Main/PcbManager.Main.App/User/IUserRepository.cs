using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.App.User
{
    public interface IUserRepository : IRepositoryBase<Domain.UserNS.User, UserId>
    {
    }
}
