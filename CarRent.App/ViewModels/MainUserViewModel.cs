using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CarRent.App.Models;
using CarRent.App.Views;
using CarRent.Common.Models;
using CarRent.Data.Models;
using CarRent.Data.Services;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CarRent.App.ViewModels
{
    public class MainUserViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingsService;
        private readonly ICarService _carService;

        public ICommand LogoutCommand { get; }
        public ICommand HandleCarSelectionCommand { get; }
        public BookingsModel Bookings { get; set; }

        private List<CarModel> cars;
        public List<CarModel> Cars
        {
            get { return cars; }
            set { cars = value; OnPropertyChanged("Cars"); }
        }

        private CarModel _selectedCar;
        public CarModel SelectedCar
        {
            get { return _selectedCar; }
            set { _selectedCar = value; OnPropertyChanged("SelectedCar"); }
        }


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

        public MainUserViewModel(IUserService userService, IBookingService bookingsService, ICarService carService)
        {
            _userService = userService;
            CurrentUserAccount = new UserAccountModel();
            LogoutCommand = new ViewModelCommand(p => ExecuteLogoutCommand());
            _bookingsService = bookingsService;
            _carService = carService;

            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userService.GetByEmail(Thread.CurrentPrincipal?.Identity?.Name);
            if (user != null)
            {
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";

                Bookings = _bookingsService.GetBookingsForUser(user.Id);
                LoadCars();
            }
            else
            {
                CurrentUserAccount.DisplayName = "Not logged in";
            }
        }

        private void LoadCars()
        {
            Cars = _carService.GetAll();
            SelectedCar = Cars[0];

        }

        private async void ExecuteLogoutCommand()
        {
            App thisApp = (App)App.Current;
            thisApp.ShowLogin();
        }

    }
}
