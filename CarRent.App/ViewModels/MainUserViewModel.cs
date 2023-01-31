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
        private UserAccountModel _currentUserAccount;
        private readonly IUserService _userService;
        private readonly IBookingService _bookingsService;
        private readonly ICarService _carService;

        public ICommand LogoutCommand { get; }
        private BookingsModel _bookings;
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
        public ICommand RemoveBooking { get; }
        public ICommand CreateBooking { get; }
        public ICommand RemoveBindingCommand { get; }

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
            set { _selectedCar = value; OnPropertyChanged("SelectedCar"); RecalculatePrice(); }
        }

        private DateTime _dateFrom = DateTime.Now;

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; 
                
                if(value.CompareTo(_dateTo) > 0)
                {
                    DateTo = value;
                }
                
                OnPropertyChanged("DateFrom"); RecalculatePrice();
            }

        }

        private DateTime _minAvailDate = DateTime.Now;
        public DateTime MinAvailDate
        {
            get { return _minAvailDate; }
            set { _minAvailDate = value; OnPropertyChanged("MinAvailDate"); }
        }


        private DateTime _dateTo = DateTime.Now.AddDays(1);
        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; OnPropertyChanged("DateTo"); RecalculatePrice(); }
        }



        private string _totalPrice = "0,00 zł";

        public string TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = $"{value} zł"; OnPropertyChanged("TotalPrice"); }

        }

        private void RecalculatePrice()
        {
            TotalPrice = _bookingsService.GetCalculatedPrice(SelectedCar.PricePerDay, DateFrom, DateTo).ToString("N2");
        }

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("SelectedIndex"); }
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
            TotalPrice = _bookingsService.GetCalculatedPrice(SelectedCar.PricePerDay, DateFrom, DateTo).ToString("N2");
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

        private  void HandleCreateBooking()
        {
             _bookingsService.CreateBooking(CurrentUserAccount.Id, SelectedCar, DateFrom, DateTo);
            Bookings = _bookingsService.GetBookingsForUser(CurrentUserAccount.Id);
            SelectedIndex = 1;
        }

    }
}
