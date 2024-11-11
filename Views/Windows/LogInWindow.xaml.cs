using DiagnoseMe.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiagnoseMe.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DiagnoseMe.Views.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy AddEventWindow.xaml
    /// </summary>
    public partial class LogInWindow : MetroWindow
    {
        public LogInVM LogInVM
        {
            get
            {
                return _loginVM;
            }
            set
            {
                _loginVM = value;
            }
        }
        private LogInVM _loginVM;

        public LogInWindow()
        {
            InitializeComponent();
            LogInVM = new LogInVM();
            this.DataContext = LogInVM;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindwVM = App.Current.Services.GetService<MainWindowVM>();
            mainWindwVM.IsUserLoggedIn = true;

            this.Close();
        }
    }
}
