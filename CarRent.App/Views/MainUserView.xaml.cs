
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
        }
    }
}
