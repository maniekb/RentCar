using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Domain.Entities;

namespace CarRent.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        User GetUserByEmail(string email);
        List<User> GetAllNonAdmin();
        bool RemoveUser(int userId);
        void AddUser(User user);
    }
}
