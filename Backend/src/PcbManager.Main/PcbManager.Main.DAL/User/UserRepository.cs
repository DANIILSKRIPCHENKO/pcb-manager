using PcbManager.Main.App.User;
using PcbManager.Main.DAL.Abstractions;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.DAL.User
{
    public class UserRepository : RepositoryBase<Domain.UserNS.User, UserId>, IUserRepository
    {
        public UserRepository(PcbManagerDbContext context) : base(context)
        {
        }
    }
}
