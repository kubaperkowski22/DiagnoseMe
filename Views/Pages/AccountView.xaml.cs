using DiagnoseMe.ViewModels;
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
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.Helpers;
using DiagnoseMe.Models;

namespace DiagnoseMe.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountVM ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
            }
        }
        private AccountVM _viewModel;
        public AccountView()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetService<AccountVM>();
            this.DataContext = ViewModel;
        }

        private void DeleteAccount_ButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteUser();
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginFirst_Grid.Visibility = Visibility.Visible;
            YourAccount_Grid.Visibility = Visibility.Collapsed;

            ViewModel.LogOut();
        }

        private void GenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (ViewModel is null)
                return;

            if (Male_RadioButton.IsChecked == true)
                ViewModel.Gender = EGender.Male;
            else
                ViewModel.Gender = EGender.Female;
        }

        private void SwitchView()
        {
            if(YourAccount_Grid.Visibility == Visibility.Visible)
            {
                YourAccount_Grid.Visibility = Visibility.Collapsed;
                EditAccount_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                YourAccount_Grid.Visibility = Visibility.Visible;
                EditAccount_Grid.Visibility = Visibility.Collapsed;
            }
        }

        private async void SaveChanges_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(EditPassword_PasswordBox.Password))
                ViewModel.User.Password = EditPassword_PasswordBox.Password;

            try
            {
                await ViewModel.UpdateUserData();
                Password_TextBlock.Text = EditPassword_PasswordBox.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nie udało się zapisać zmian. Spróbuj ponownie później.\n {ex.Message}", "Nie udało się zapisać zmian", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            SwitchView();
        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {
            SwitchView();
        }

        private void EditData_Button_Click(object sender, RoutedEventArgs e)
        {
            SwitchView();
        }
    }
}
