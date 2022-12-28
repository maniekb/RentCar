using System.Threading.Tasks;
using CarRent.Domain.Entities;

namespace CarRent.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
