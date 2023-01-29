using CarRent.App.Models;
using CarRent.Data.Models;
using CarRent.Data.Services.Abstract;
using System.Collections.Generic;
using System.Threading;
using CarRent.Common.Models;
using System.Windows.Input;
using System.Linq;

namespace CarRent.App.ViewModels
{
    public class MainAdminViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly IBookingService _bookingsService;
        public ICommand LogoutCommand { get; }
        public ICommand RemoveUser { get;  }
        public ICommand ShowUserBookings { get; }

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

        public List<CarModel> Cars { get; set; }
        public List<UserModel> Users { get; set; }
        private BookingsModel _bookings;
        public BookingsModel Bookings {
            get
            {
                return _bookings;
            }
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public MainAdminViewModel(IUserService userService, ICarService carService, IBookingService bookingsService)
        {
            _userService = userService;
            _carService = carService;
            _bookingsService = bookingsService;
            CurrentUserAccount = new UserAccountModel();
            LogoutCommand = new ViewModelCommand(p => ExecuteLogoutCommand());
            RemoveUser = new ViewModelCommand(p => ExecuteRemoveUserCommand(p));

            LoadCurrentUserData();
            LoadViewData();
        }

        private void LoadViewData()
        {
            Cars = _carService.GetAll();
            Users = _userService.GetAllNonAdmin();
            Bookings = _bookingsService.GetBookings();
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

        private async void ExecuteLogoutCommand()
        {
            App thisApp = (App)App.Current;
            thisApp.ShowLogin();
        }

        private async void ExecuteRemoveUserCommand(object obj)
        {
            var first = Bookings.UpcomingBookings.First(x => x.Id == (int)obj);
            
            _bookingsService.RemoveBooking((int)obj);
            Bookings = _bookingsService.GetBookings();
        }

    }
}
