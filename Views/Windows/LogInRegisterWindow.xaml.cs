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
using DiagnoseMe.Helpers;

namespace DiagnoseMe.Views.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy AddEventWindow.xaml
    /// </summary>
    public partial class LogInRegisterWindow : MetroWindow
    {
        public LogInRegisterVM LogInVM
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
        private LogInRegisterVM _loginVM;

        public LogInRegisterWindow()
        {
            InitializeComponent();
            LogInVM = new LogInRegisterVM();
            this.DataContext = LogInVM;
        }

        private void ShowRegisterView_Button_Click(object sender, RoutedEventArgs e)
        {
            SetRegisterView();
        }

        private void ShowLogInView_Button_Click(object sender, RoutedEventArgs e)
        {
            SetLogInView();
        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindwVM = App.Current.Services.GetService<MainWindowVM>();
            mainWindwVM.IsUserLoggedIn = true;

            this.Close();
        }

        private void SetRegisterView()
        {
            LogIn_Grid.Visibility = Visibility.Collapsed;
            Register_Grid.Visibility = Visibility.Visible;
            Height = 600;
            Width = 500;
        }

        private void SetLogInView()
        {
            LogIn_Grid.Visibility = Visibility.Visible;
            Register_Grid.Visibility = Visibility.Collapsed;
            Height = 320;
            Width = 480;
        }

        private void GenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (LogInVM is null)
                return;

            if (Male_RadioButton.IsChecked == true)
                LogInVM.Gender = EGender.Male;
            else
                LogInVM.Gender = EGender.Female;
        }
    }
}
