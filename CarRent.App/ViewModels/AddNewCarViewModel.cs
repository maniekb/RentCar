using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CarRent.Data.Services.Abstract;
using System.Windows.Input;
using CarRent.Domain.Enums;

namespace CarRent.App.ViewModels
{
    public class AddNewCarViewModel : ViewModelBase
    {
        //Fields
        private readonly ICarService _carService;
        private MainAdminViewModel AdminViewModel;
        public ICommand AddNewCar { get; }

        public static ObservableCollection<KeyValuePair<int, string>> FuelTypesCollection { get; set; } = new ObservableCollection<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>((int)FuelTypeEnum.DIESEL ,"Diesel"),
            new KeyValuePair<int, string>((int)FuelTypeEnum.PETROL ,"Benzyna"),
            new KeyValuePair<int, string>((int)FuelTypeEnum.HYBRID ,"Hybryda"),
            new KeyValuePair<int, string>((int)FuelTypeEnum.ELECTRIC ,"Elektryczny"),
        };

        public static ObservableCollection<KeyValuePair<int, string>> CarClassCollection { get; set; } = new ObservableCollection<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>((int)CarClassEnum.BUDGET ,"Budzet"),
            new KeyValuePair<int, string>((int)CarClassEnum.NORMAL ,"Normalne"),
            new KeyValuePair<int, string>((int)CarClassEnum.PREMIUM ,"Premium"),
        };

        public static ObservableCollection<KeyValuePair<int, int>> YearsCollection { get; set; } = new ObservableCollection<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int, int>(2015,2015),
            new KeyValuePair<int, int>(2016,2016),
            new KeyValuePair<int, int>(2017,2017),
            new KeyValuePair<int, int>(2018,2018),
            new KeyValuePair<int, int>(2019,2019),
            new KeyValuePair<int, int>(2020,2020),
            new KeyValuePair<int, int>(2021,2021),
            new KeyValuePair<int, int>(2022,2022)
        };

        public virtual string Number { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Model { get; set; }
        public virtual KeyValuePair<int, int> YearOfProduction { get; set; } = YearsCollection.Last();
        public virtual KeyValuePair<int, string> CarClass { get; set; } = CarClassCollection.First();
        public virtual KeyValuePair<int, string> FuelType { get; set; } = FuelTypesCollection.First();
        public virtual string PricePerDay { get; set; }


        public virtual string ErrorMessage { get; set; }

        public AddNewCarViewModel(ICarService carService, MainAdminViewModel adminViewModel)
        {
            _carService = carService;
            AdminViewModel = adminViewModel;
            AddNewCar = new ViewModelCommand(p => ExecuteAddNewCar(p));
        }

        private void ExecuteAddNewCar(object obj)
        {
            if(DataIsIncomplete())
            {
                MessageBox.Show("Nie wszystkie dane są uzupełnione poprawnie.", "Niepoprawne dane", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_carService.AddCar(Number, Brand, Model, (CarClassEnum) CarClass.Key, (FuelTypeEnum) FuelType.Key,
                YearOfProduction.Value, Decimal.Parse(PricePerDay)))
            {
                MessageBox.Show($"Samochód o tym numerze rejestracyjnym {Number.ToUpper()} już istnieje.", "Niepoprawne dane", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            MessageBox.Show($"Dodano samochód o numerze rejestracyjnym {Number.ToUpper()}.", "Dodano samochód");
            AdminViewModel.LoadCars();
        }

        private bool DataIsIncomplete() =>
            string.IsNullOrEmpty(Number)
            || string.IsNullOrEmpty(Brand)
            || string.IsNullOrEmpty(Model)
            || string.IsNullOrEmpty(PricePerDay)
            || !Decimal.TryParse(PricePerDay, out var price);
    }
}
