using System.Threading;
using System.Threading.Tasks;
using CarRent.App.Models;
using CarRent.Data.Services;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.ViewModels
{
    public class MainViewModel : ViewModelBase
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

        public MainViewModel(IUserService userService)
        {
            userService = userService;
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private async Task LoadCurrentUserData()
        {
            var user = await _userService.GetByEmail(Thread.CurrentPrincipal?.Identity?.Name);
            if (user != null)
            {
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}.)";
            }
            else
            {
                CurrentUserAccount.DisplayName = "Not logged in";
                //Hide child views.
            }
        }
    }
}
