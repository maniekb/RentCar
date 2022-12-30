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
        }
    }
}
