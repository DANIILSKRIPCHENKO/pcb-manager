using PcbManager.App.User;
using PcbManager.DAL.Abstractions;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.DAL.User
{
    public class UserRepository : RepositoryBase<Domain.UserNS.User, UserId>, IUserRepository
    {
        public UserRepository(PcbManagerDbContext context) : base(context)
        {
        }
    }
}
