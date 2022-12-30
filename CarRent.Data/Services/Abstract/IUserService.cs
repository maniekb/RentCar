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
    }
}
