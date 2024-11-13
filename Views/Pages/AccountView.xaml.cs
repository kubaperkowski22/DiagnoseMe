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
    }
}
