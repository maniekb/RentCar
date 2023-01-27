using System.Collections.Generic;
using System.Windows;
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
    }
}
