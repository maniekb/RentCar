using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CarRent.App.ViewModels;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for MainAdminView.xaml
    /// </summary>F
    public partial class MainAdminView : Window
    {
        public MainAdminView(MainAdminViewModel mainAdminViewModel)
        {
            InitializeComponent();
            DataContext = mainAdminViewModel;
        }

        private void removeBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainAdminViewModel)DataContext).RemoveBooking.Execute(((Button)sender).Tag);
        }


        private void previewBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainAdminViewModel)DataContext).ShowUserBookings.Execute(((Button)sender).Tag);
        }

        private void removeUserBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainAdminViewModel)DataContext).RemoveUser.Execute(((Button)sender).Tag);

        }

    }
}
