using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows;
using CarRent.App.ViewModels;
using CarRent.App.Views;
using CarRent.Common.Authentication.Constants;
using CarRent.Data.Persistence;
using CarRent.Data.Repositories;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services;
using CarRent.Data.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
