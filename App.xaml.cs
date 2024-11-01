using System.Configuration;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.ViewModels;
using DiagnoseMe.Views;
using DiagnoseMe.Helpers;

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

            //Helpers
            services.AddSingleton<Diagnosis>();

            return services.BuildServiceProvider();
        }

    }
}