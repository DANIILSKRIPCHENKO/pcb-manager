using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using Microsoft.EntityFrameworkCore;
using PcbManager.App;
using PcbManager.Domain.UserNS.ValueObjects;

namespace PcbManager.DAL.User
{
    public class UserRepository : IUserRepository
    {
        private readonly PcbManagerDbContext _context;

        public UserRepository(PcbManagerDbContext context)
        {
           _context = context;
        }

        public async Task<Result<Domain.UserNS.User>> CreateAsync(Domain.UserNS.User user)
        {
           var entityEntry = await _context.Users.AddAsync(user);

           await _context.SaveChangesAsync();

           return Result<Domain.UserNS.User>.Success(entityEntry.Entity);
        }

        public async Task<Maybe<List<Domain.UserNS.User>>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync<Domain.UserNS.User>();

            return Maybe<List<Domain.UserNS.User>>.From(users);
        }

        public async Task<Maybe<Domain.UserNS.User>> GetByIdAsync(UserId id)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync<Domain.UserNS.User>(x => x.Id == id);

            return Maybe<Domain.UserNS.User>.From(user);
        }
    }
}
