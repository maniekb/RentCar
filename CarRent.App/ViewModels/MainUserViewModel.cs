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
using System.ComponentModel;
using CarRent.Data.Migrations;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace CarRent.App.ViewModels
{
    public class MainUserViewModel : ViewModelBase
    {
        //Fields
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingsService;

        private List<CarModel> cars;
        private CarModel _selectedCar;
        private BookingsModel _bookings;
        private UserAccountModel _currentUserAccount;

        private DateTime _dateFrom = DateTime.Now;
        private DateTime _minAvailDate = DateTime.Now;
        private DateTime _dateTo = DateTime.Now.AddDays(1);

        private int _selectedTabIndex = 0;
        private int _userBookingCount = 0;
        private string _totalPrice = "0,00 zł";
        private bool _isBookinkAvailable = true;
        private bool _isAvailabilityErrorVisible = false;

        public ICommand LogoutCommand { get; }
        public ICommand RemoveBooking { get; }
        public ICommand CreateBooking { get; }
        public ICommand RemoveBindingCommand { get; }

        public MainUserViewModel(IUserService userService, IBookingService bookingsService, ICarService carService)
        {
            CurrentUserAccount = new UserAccountModel();

            LogoutCommand = new ViewModelCommand(p => ExecuteLogoutCommand());
            CreateBooking = new ViewModelCommand(p => HandleCreateBooking());
            RemoveBooking = new ViewModelCommand(p => ExecuteRemovBookingCommand(p));

            _carService = carService;
            _userService = userService;
            _bookingsService = bookingsService;

            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userService.GetByEmail(Thread.CurrentPrincipal?.Identity?.Name);
            if (user != null)
            {
                CurrentUserAccount.Email = user.Email;
                CurrentUserAccount.Id = user.Id;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";

                ReloadViewData();
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
            TotalPrice = _bookingsService.GetCalculatedPrice(SelectedCar.PricePerDay, DateFrom, DateTo).ToString("N2");
        }

        public bool IsBookinkAvailable
        {
            get { return _isBookinkAvailable; }
            set { _isBookinkAvailable = value; IsAvailabilityErrorVisible = !value; OnPropertyChanged("IsBookinkAvailable");  }
        }

        public bool IsAvailabilityErrorVisible
        {
            get { return _isAvailabilityErrorVisible; }
            set { _isAvailabilityErrorVisible = value; OnPropertyChanged("IsAvailabilityErrorVisible"); }

        }

        public BookingsModel Bookings
        {
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

        public List<CarModel> Cars
        {
            get { return cars; }
            set { cars = value; OnPropertyChanged("Cars"); }
        }

        public CarModel SelectedCar
        {
            get { return _selectedCar; }
            set { _selectedCar = value; OnPropertyChanged("SelectedCar"); RefreshAvailability(); }
        }

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set
            {
                _dateFrom = value;

                if (value.CompareTo(_dateTo) > 0)
                {
                    DateTo = value;
                }

                OnPropertyChanged("DateFrom"); RefreshAvailability();
            }

        }

        public DateTime MinAvailDate
        {
            get { return _minAvailDate; }
            set { _minAvailDate = value; OnPropertyChanged("MinAvailDate"); }
        }


        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; OnPropertyChanged("DateTo"); RefreshAvailability(); }
        }
        
        
        public int UserBookingCount
        {
            get { return _userBookingCount; }
            set { _userBookingCount = value; OnPropertyChanged("UserBookingCount");}
        }


        public string TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = $"{value} zł"; OnPropertyChanged("TotalPrice"); }

        }

        private void RefreshAvailability()
        {
            RecalculatePrice();
            if (IsBookinkAvailable == false) IsBookinkAvailable = true;
        }

        private void RecalculatePrice()
        {
            TotalPrice = _bookingsService.GetCalculatedPrice(SelectedCar.PricePerDay, DateFrom, DateTo).ToString("N2");
        }


        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value; OnPropertyChanged("SelectedTabIndex"); }
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

        private async void ExecuteLogoutCommand()
        {
            App thisApp = (App)App.Current;
            thisApp.ShowLogin();
        }

        private async void ExecuteRemovBookingCommand(object obj)
        {
            _bookingsService.RemoveBooking((int)obj);
            ReloadViewData();
        }

        private void ReloadViewData()
        {
            Bookings = _bookingsService.GetBookingsForUser(CurrentUserAccount.Id);
            UserBookingCount = _bookingsService.GetBookingCountForUser(CurrentUserAccount.Id);
        }

        private void HandleCreateBooking()
        {
            try
            {
                _bookingsService.CreateBooking(CurrentUserAccount.Id, SelectedCar, DateFrom, DateTo);
                ReloadViewData();
                SelectedTabIndex = 1;
            }
            catch (Exception)
            {
                IsBookinkAvailable = false;
            }
        }

    }
}
