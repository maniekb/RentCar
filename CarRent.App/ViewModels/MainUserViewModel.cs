using System.Threading;
using CarRent.App.Models;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.ViewModels
{
    public class MainUserViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainUserViewModel(IUserService userService)
        {
            _userService = userService;
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userService.GetByEmail(Thread.CurrentPrincipal?.Identity?.Name);
            if (user != null)
            {
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}.";
            }
            else
            {
                CurrentUserAccount.DisplayName = "Not logged in";
            }
        }
    }
}
