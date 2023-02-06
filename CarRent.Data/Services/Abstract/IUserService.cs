using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarRent.App.Models;
using CarRent.Common.Authentication.Results;

namespace CarRent.Data.Services.Abstract
{
    public interface IUserService
    {
        Task<AuthenticationResult> AuthenticateUser(NetworkCredential networkCredential);
        UserModel GetByEmail(string email);
        List<UserModel> GetAllNonAdmin();

        bool RemoveUser(int userId);
        void AddUser(NetworkCredential networkCredential, string name, string lastName);
    }
}
