using CarRent.App.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void removeBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainUserViewModel)DataContext).RemoveBooking.Execute(((Button)sender).Tag);
        }
    }
}
