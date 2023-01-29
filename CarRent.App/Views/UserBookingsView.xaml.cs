using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarRent.App.ViewModels;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for UserBookings.xaml
    /// </summary>
    public partial class UserBookingsView : Window
    {
        public UserBookingsView(UserBookingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void removeBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            ((UserBookingsViewModel)DataContext).RemoveBooking.Execute(((Button)sender).Tag);
        }
    }
}
