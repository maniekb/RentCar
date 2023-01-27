using System.Collections.Generic;
using System.Windows;
using CarRent.App.ViewModels;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for MainAdminView.xaml
    /// </summary>
    public partial class MainAdminView : Window
    {
        public MainAdminView(MainAdminViewModel mainAdminViewModel)
        {
            InitializeComponent();
            DataContext = mainAdminViewModel;

            List<Rent> items = new List<Rent>();
            items.Add(new Rent() { CarName = "Fiat punto", UserName="User 1", From = "21.01.2022", To = "22.02.2022" });
            items.Add(new Rent() { CarName = "Fiat punto", UserName = "User 1", From = "21.01.2022", To = "22.02.2022" });
            items.Add(new Rent() { CarName = "Fiat punto", UserName = "User 1", From = "21.01.2022", To = "22.02.2022" });
            rentsDataBinding.ItemsSource = items;

            List<User> users = new List<User>();
            users.Add(new User() { Email = "user@user.com", Name = "User 1" });
            users.Add(new User() { Email = "user@user.com", Name = "User 1" });
            users.Add(new User() { Email = "user@user.com", Name = "User 1" });
            users.Add(new User() { Email = "user@user.com", Name = "User 1" });
            users.Add(new User() { Email = "user@user.com", Name = "User 1" });
            usersDataBinding.ItemsSource = items;

            List<Car> cars = new List<Car>();
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            cars.Add(new Car() { Model = "Fiat", Type = "Punto", Fuel = "45/45" });
            carsDataBinding.ItemsSource = cars;
        }
    }

    public class Car
    {
        public string Model { get; set; }
        public string Type { get; set; }

        public string Fuel { get; set; }

    }

    public class Rent
    {
        public string CarName { get; set; }
        public string UserName { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

}
