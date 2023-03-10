using System.Windows;
using System.Windows.Input;
using CarRent.App.ViewModels;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }

        private async void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ////var vm = new SignUpViewModel(((LoginViewModel)DataContext)._userService);
            var view = new SignUpView(new SignUpViewModel(((LoginViewModel)DataContext)._userService));
            //view.Topmost = true;
            //var view = new LoginView(new LoginViewModel(((LoginViewModel)DataContext)._userService));
            view.Show();
        }
    }
}
