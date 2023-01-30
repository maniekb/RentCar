using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CarRent.App.ViewModels;
using CarRent.Domain.Enums;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for NewCarFormView.xaml
    /// </summary>
    public partial class NewCarFormView : Window
    {
        public NewCarFormView(AddNewCarViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void addNewCarBtn_Click(object sender, RoutedEventArgs e)
        {
            ((AddNewCarViewModel)DataContext).AddNewCar.Execute(((Button)sender).Tag);
        }
    }
}
