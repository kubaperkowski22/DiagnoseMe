using DiagnoseMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiagnoseMe.ViewModels
{
    public class NotificationsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        public NotificationsVM()
        {
            Notifications = new ObservableCollection<Notification>();
        }

        public void AddAppointment(Notification notifications)
        {
            Notifications.Add(notifications);
            OnPropertyChanged(nameof(Notifications));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
