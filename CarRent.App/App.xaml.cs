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
            RunMigrations(ServiceProvider);
            ShowLogin();
        }

        public void ShowLogin()
        {
            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var isAdmin = Thread.CurrentPrincipal.IsInRole(AuthConstants.AdminRole);
                    Window mainWindow;
                    if (isAdmin)
                    {
                        mainWindow = ServiceProvider.GetRequiredService<MainAdminView>();
                    }
                    else
                    {
                        mainWindow = ServiceProvider.GetRequiredService<MainUserView>();
                    }

                    mainWindow.Show();
                    loginView.Close();
                }
            };
        }

        private void RunMigrations(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<CarRentDbContext>())
            {
                context.Database.Migrate();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Views
            services.AddTransient(typeof(LoginView));
            services.AddTransient(typeof(MainAdminView));
            services.AddTransient(typeof(MainUserView));

            // ViewModels
            services.AddTransient(typeof(LoginViewModel));
            services.AddTransient(typeof(MainAdminViewModel));
            services.AddTransient(typeof(MainUserViewModel));

            // Persistence
            services.AddDbContext<CarRentDbContext>
            (options => options.UseNpgsql(
                Configuration.GetConnectionString("Database")), ServiceLifetime.Transient, ServiceLifetime.Transient);


            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}