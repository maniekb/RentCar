using CarRent.App.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
