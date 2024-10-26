using CommunityToolkit.Mvvm.ComponentModel;
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
    public class AppointmentsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Appointment> Appointments {  get; set; }

        public AppointmentsVM()
        {
            Appointments = new ObservableCollection<Appointment>();
        }

        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
            OnPropertyChanged(nameof(Appointments));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
