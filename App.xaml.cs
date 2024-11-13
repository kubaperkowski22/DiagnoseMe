using System.Configuration;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.ViewModels;
using DiagnoseMe.Views;
using DiagnoseMe.Tools.Diagnose;
using DiagnoseMe.Tools.Data;
using Microsoft.EntityFrameworkCore;

namespace DiagnoseMe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            InitializeComponent();
        }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AppointmentsView>();

            //ViewModels
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<AppointmentsVM>();
            services.AddSingleton<AccountVM>();

            //Helpers
            services.AddSingleton<Diagnosis>();

            //Database
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("REDACTED_CONNECTION_STRING"));

            return services.BuildServiceProvider();
        }

    }
}