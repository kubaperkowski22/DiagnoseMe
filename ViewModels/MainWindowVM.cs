using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DiagnoseMe.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnoseMe.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public User LoggedUser
        {
            get => _loggedUser;
            set
            {
                _loggedUser = value;

                OnPropertyChanged(nameof(LoggedUser));
            }
        }
        private User _loggedUser;


        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set
            {
                _isUserLoggedIn = value;
            }
        }
        private bool _isUserLoggedIn;

        public MainWindowVM() 
        {
            IsUserLoggedIn = false;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
