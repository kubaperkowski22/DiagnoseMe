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
        public User? LoggedUser
        {
            get => _loggedUser;
            set
            {
                _loggedUser = value;

                if (_loggedUser is null)
                {
                    IsUserLoggedIn = false;
                    NotificationsVM.StopTimer();
                }
                else
                {
                    IsUserLoggedIn = true;
                    NotificationsVM.StartTimer();
                    AppointmentsVM.RefreshAppointments();
                    NotificationsVM.RefreshNotifications();
                }

                OnPropertyChanged(nameof(LoggedUser));
            }
        }
        private User? _loggedUser;

        public AppointmentsVM AppointmentsVM
        {
            get
            {
                return _appointmentsVM;
            }
            set
            {
                _appointmentsVM = value;

                if (_appointmentsVM is null)
                    _appointmentsVM = App.Current.Services.GetService<AppointmentsVM>();

                OnPropertyChanged(nameof(NotificationsVM));
            }
        }
        private AppointmentsVM _appointmentsVM;

        public NotificationsVM NotificationsVM
        {
            get
            {
                return _notificationsVM;
            }
            set
            {
                _notificationsVM = value;

                if (_notificationsVM is null)
                    _notificationsVM = App.Current.Services.GetService<NotificationsVM>();

                OnPropertyChanged(nameof(NotificationsVM));
            }
        }
        private NotificationsVM _notificationsVM;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set
            {
                _isUserLoggedIn = value;
            }
        }
        private bool _isUserLoggedIn;

        public bool AreActiveNotifications
        {
            get => NotificationsVM.Notifications.Count > 0 ? true : false;
        }

        public MainWindowVM() 
        {
            IsUserLoggedIn = false;
            NotificationsVM = App.Current.Services.GetService<NotificationsVM>();
            AppointmentsVM = App.Current.Services.GetService<AppointmentsVM>();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
