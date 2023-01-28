using System.Threading;
using System.Windows.Input;
using CarRent.App.Models;
using CarRent.App.Views;
using CarRent.Common.Models;
using CarRent.Data.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace CarRent.App.ViewModels
{
    public class MainUserViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingsService;

        public ICommand LogoutCommand { get; }
        public BookingsModel Bookings { get; set; }

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

        public MainUserViewModel(IUserService userService, IBookingService bookingsService)
        {
            _userService = userService;
            CurrentUserAccount = new UserAccountModel();
            LogoutCommand = new ViewModelCommand(p => ExecuteLogoutCommand());
            _bookingsService = bookingsService;


            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userService.GetByEmail(Thread.CurrentPrincipal?.Identity?.Name);
            if (user != null)
            {
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}.";

                Bookings = _bookingsService.GetBookingsForUser(user.Id);

            }
            else
            {
                CurrentUserAccount.DisplayName = "Not logged in";
            }
        }

        private async void ExecuteLogoutCommand()
        {
            App thisApp = (App)App.Current;
            thisApp.ShowLogin();
        }

    }
}
