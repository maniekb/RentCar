using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarRent.App.Models;
using CarRent.Common.Authentication.Helpers;
using CarRent.Common.Authentication.Results;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services.Abstract;

namespace CarRent.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> AuthenticateUser(NetworkCredential networkCredential)
        {
            var user = await _userRepository.GetUserByEmailAsync(networkCredential.UserName);
            if (user is null)
                return AuthenticationResult.Unauthenticated;

            var passwordHash = PasswordHasher.Hash(networkCredential.Password);
            if(!passwordHash.Equals(user.Password))
                return AuthenticationResult.Unauthenticated;

            if (user.IsAdmin)
                return AuthenticationResult.AuthenticatedAdmin;

            return AuthenticationResult.Authenticated;
        }

        public UserModel GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var user = _userRepository.GetUserByEmail(email);
            if (user is null)
                return null;

            var model = new UserModel()
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Email = user.Email,
                LastName = user.LastName,
                Name = user.Name
            };

            return model;
        }

        public List<UserModel> GetAllNonAdmin()
        {
            var users = _userRepository.GetAllNonAdmin();

            return users.Select(u => new UserModel()
            {
                Id = u.Id,
                IsAdmin = u.IsAdmin,
                Email = u.Email,
                LastName = u.LastName,
                Name = u.Name
            }).ToList();
        }
    }
}
