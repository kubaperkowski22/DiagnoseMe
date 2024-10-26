using DiagnoseMe.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagnoseMe.Views
{
    /// <summary>
    /// Logika interakcji dla klasy LogInView.xaml
    /// </summary>
    public partial class LogInView : UserControl
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
        public LogInView()
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
        }

        private void ForgotPassword_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
