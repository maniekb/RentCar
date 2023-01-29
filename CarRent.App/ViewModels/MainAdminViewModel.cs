using CarRent.App.Models;
using CarRent.Data.Models;
using CarRent.Data.Services.Abstract;
using System.Collections.Generic;
using System.Threading;
using CarRent.Common.Models;
using System.Windows.Input;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CarRent.App.Views;

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
        public ICommand RemoveBooking { get; }
        public ICommand RemoveUser { get; }
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
        private List<UserModel> _users;
        public List<UserModel> Users { 
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
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
            RemoveBooking = new ViewModelCommand(p => ExecuteRemovBookingCommand(p));
            RemoveUser = new ViewModelCommand(p => ExecuteRemoveUser(p));
            ShowUserBookings = new ViewModelCommand(p => ExecuteShowUserBookings(p));
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

        private async void ExecuteRemovBookingCommand(object obj)
        {
            
            _bookingsService.RemoveBooking((int)obj);
            Bookings = _bookingsService.GetBookings();
        }

        private async void ExecuteShowUserBookings(object obj)
        {  
            var bookingsPreview = new UserBookingsView(new UserBookingsViewModel((int)obj, _bookingsService));
            bookingsPreview.Show();
        }

        private async void ExecuteRemoveUser(object obj)
        {
            _userService.RemoveUser((int)obj);
            Users = _userService.GetAllNonAdmin();
        }

    }
}
