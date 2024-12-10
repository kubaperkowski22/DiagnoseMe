using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnoseMe.ViewModels
{
    public class SettingsVM : INotifyPropertyChanged
    {
        public MainWindowVM MainWindowVM
        {
            get
            {
                return _mainWindowVM;
            }
            set
            {
                _mainWindowVM = value;

                OnPropertyChanged(nameof(MainWindowVM));
                OnPropertyChanged(nameof(IsUserLoggedIn));
            }
        }
        private MainWindowVM _mainWindowVM;

        public bool IsUserLoggedIn
        {
            get => MainWindowVM.IsUserLoggedIn;
        }

        public SettingsVM()
        {
            MainWindowVM = App.Current.Services.GetService<MainWindowVM>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
