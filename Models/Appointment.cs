using DiagnoseMe.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiagnoseMe.Models
{
    public class Appointment : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _name;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }
        private DateTime _dateTime;
        public string? Localization
        {
            get => _localization;
            set
            {
                _localization = value;
                OnPropertyChanged("Localization");
            }
        }
        private string? _localization;
        public bool IsFinished
        {
            get => DateTime.UtcNow > DateTime;
        }

        public Appointment()
        {

        }
        public Appointment(string name, DateTime dateTime, string? localization = null)
        {
            UserId = App.Current.Services.GetService<MainWindowVM>().LoggedUser.Id;
            Name = name;
            DateTime = dateTime;
            Localization = localization;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
