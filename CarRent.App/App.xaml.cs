using System;
using System.Configuration;
using System.IO;
using System.Windows;
using CarRent.App.ViewModels;
using CarRent.App.Views;
using CarRent.Data.Persistence;
using CarRent.Data.Repositories;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services;
using CarRent.Data.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRent.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Views
            services.AddTransient(typeof(LoginView));
            services.AddTransient(typeof(LoginViewModel));

            // Persistence
            services.AddDbContext<CarRentDbContext>
            (options => options.UseNpgsql(
                Configuration.GetConnectionString("Database")));

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}