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
            services.AddSingleton<HistoryView>();

            //ViewModels
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<AppointmentsVM>();
            services.AddSingleton<AccountVM>();
            services.AddSingleton<HistoryVM>();

            //Helpers
            services.AddSingleton<Diagnosis>();

            //Database
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(GetConnectionString()));

            return services.BuildServiceProvider();
        }

        public static string GetConnectionString()
        {
            return "Server=tcp:diagnoseme.database.windows.net,1433;Initial Catalog=DiagnoseMe;Persist Security Info=False;User ID=diagnoseme;Password=Diagnose1@34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}