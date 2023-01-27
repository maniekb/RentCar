using CarRent.App.Models;
using CarRent.Data.Models;
using CarRent.Data.Services.Abstract;
using System.Collections.Generic;
using System.Threading;
using CarRent.Common.Models;

namespace CarRent.App.ViewModels
{
    public class MainAdminViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly IBookingService _bookingsService;

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
        public BookingsModel Bookings { get; set; }

        public MainAdminViewModel(IUserService userService, ICarService carService, IBookingService bookingsService)
        {
            _userService = userService;
            _carService = carService;
            _bookingsService = bookingsService;
            CurrentUserAccount = new UserAccountModel();
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
    }
}
