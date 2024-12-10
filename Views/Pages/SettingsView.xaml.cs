using DiagnoseMe.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logika interakcji dla klasy SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl, INotifyPropertyChanged
    {
        public SettingsVM SettingsVM
        {
            get
            {
                return _settingsVM;
            }
            set
            {
                _settingsVM = value;

                OnPropertyChanged(nameof(SettingsVM));
            }
        }
        private SettingsVM _settingsVM;

        public SettingsView()
        {
            InitializeComponent();

            SettingsVM = App.Current.Services.GetService<SettingsVM>();
            this.DataContext = SettingsVM;

            AccountView_Control.DataContext = SettingsVM.MainWindowVM.LoggedUser;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeTheme_Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Services.GetService<MainWindow>().SwitchTheme();
        }
    }
}
