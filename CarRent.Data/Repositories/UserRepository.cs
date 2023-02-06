using System.Collections.Generic;
using System.Linq;
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

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public List<User> GetAllNonAdmin()
        {
            return _context.Users.Where(x => !x.IsAdmin).ToList();
        }

        public bool RemoveUser(int userId)
        {
            var local = _context.Set<User>()
               .Local
               .FirstOrDefault(entry => entry.Id.Equals(userId));

            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
                _context.Users.Remove(local);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddUser(User user)
        {
            _context.Add<User>(user);
            _context.SaveChanges();
        }
    }
}
