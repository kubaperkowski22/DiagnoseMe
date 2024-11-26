using CommunityToolkit.Mvvm.ComponentModel;
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

namespace DiagnoseMe.ViewModels
{
    public class AppointmentsVM : INotifyPropertyChanged
    {
        private AppDbContext _db;
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

        public AppointmentsVM()
        {
            _db = App.Current.Services.GetService<AppDbContext>();
        }

        public async Task AddAppointment(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            Appointments.Add(appointment);

            await _db.SaveChangesAsync();
            OnPropertyChanged(nameof(Appointments));
        }

        public async Task RemoveAppointment(Appointment appointment)
        {
            _db.Appointments.Remove(appointment);
            Appointments.Remove(appointment);

            await _db.SaveChangesAsync();
            OnPropertyChanged(nameof(Appointments));
        }

        public void RefreshAppointments()
        {
            if (_LoggedUser is not null)
            {
                var appointments = _db.Appointments.Where(x => x.UserId == _LoggedUser.Id).ToList();
                Appointments = new ObservableCollection<Appointment>(appointments);
                OnPropertyChanged(nameof(Appointments));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
