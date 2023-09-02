using PcbManager.App.Abstractions;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.App.User
{
    public interface IUserRepository: IRepositoryBase<Domain.UserNS.User, UserId>
    {
    }
}
