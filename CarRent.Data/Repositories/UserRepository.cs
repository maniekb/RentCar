using System.Threading.Tasks;
using CarRent.Data.Persistence;
using CarRent.Data.Repositories.Abstract;
using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentDbContext _context;

        public UserRepository(CarRentDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
