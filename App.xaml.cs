using System.Configuration;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.ViewModels;

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

            //ViewModels
            services.AddSingleton<MainWindowVM>();

            return services.BuildServiceProvider();
        }

    }
}