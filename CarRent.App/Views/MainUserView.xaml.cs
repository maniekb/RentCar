
using System.Collections.Generic;
using System.Windows;
using CarRent.App.ViewModels;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for MainUserView.xaml
    /// </summary>
    public partial class MainUserView : Window
    {
        public MainUserView(MainUserViewModel mainUserViewModel)
        {
            InitializeComponent();
            DataContext = mainUserViewModel;

            List<Rent> items = new List<Rent>();
            items.Add(new Rent() { CarName = "Fiat punto", From = "21.01.2022", To = "22.02.2022" });
            items.Add(new Rent() { CarName = "Fiat punto", From = "21.01.2022", To = "22.02.2022" });
            items.Add(new Rent() { CarName = "Fiat punto", From = "21.01.2022", To = "22.02.2022" });
            lvDataBinding.ItemsSource = items;

        }
    }

}
