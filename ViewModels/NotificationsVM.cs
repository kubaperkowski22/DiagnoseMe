using DiagnoseMe.Models;
using DiagnoseMe.Tools.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace DiagnoseMe.ViewModels
{
    public class NotificationsVM : INotifyPropertyChanged
    {
        private AppDbContext _db;
        private Timer _timer;
        private int _lastHour;
        private User _LoggedUser
        {
            get
            {
                if (_loggedUser is null)
                    _loggedUser = App.Current.Services.GetService<MainWindowVM>().LoggedUser;

                return _loggedUser;
            }
        }
        private User _loggedUser;
        public ObservableCollection<Appointment> Appointments { get; set; }

        public ObservableCollection<Notification> Notifications { get; set; }

        public AppointmentsVM AppointmentsVM { get; set; }

        public NotificationsVM()
        {
            Notifications = new ObservableCollection<Notification>();
            Appointments = new ObservableCollection<Appointment>();
            AppointmentsVM = App.Current.Services.GetService<AppointmentsVM>();

            _lastHour = DateTime.Now.Hour;
        }

        public void StartTimer()
        {
            _timer = new Timer(300000);    // 5 minut

            _timer.Elapsed += CheckIfHourChanged;
            _timer.Start();

            _lastHour = DateTime.Now.Hour;
        }

        public void StopTimer()
        {
            _timer.Dispose();
            _lastHour = DateTime.Now.Hour;
        }

        public void AddNotification(Notification notifications)
        {
            Notifications.Add(notifications);
            OnPropertyChanged(nameof(Notifications));
        }

        private void CheckIfHourChanged(object sender, ElapsedEventArgs e)
            {
            if (_LoggedUser is null)
                return;

            int currentHour = DateTime.Now.Hour;

            if (currentHour != _lastHour)
            {
                _lastHour = currentHour;

                RefreshNotifications();
            }
        }

        public void RefreshNotifications()
        {
            Notifications.Clear();
            AppointmentsVM.RefreshAppointments();
            Appointments = AppointmentsVM.Appointments;

            foreach (var item in Appointments)
            {
                TimeSpan timeSpan = item.DateTime - DateTime.Now;
                if (timeSpan.TotalDays <= 1 && item.UserId == _LoggedUser.Id)
                {
                    AddNotification(new Notification("Masz zbliżające się wydarzenie. Sprawdź sekcję wizyt lekarskich.", DateTime.Now));
                }
            }

            OnPropertyChanged(nameof(Notifications));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
