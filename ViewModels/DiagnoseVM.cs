using DiagnoseMe.Helpers;
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
    public class DiagnoseVM : INotifyPropertyChanged
    {
        public ObservableCollection<string> SelectedSymptoms
        {
            get => _selectedSymptoms;
            set
            {
                _selectedSymptoms = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _selectedSymptoms;

        public Diagnosis Diagnosis { get; set; }

        public DiagnoseVM()
        {
            Diagnosis = App.Current.Services.GetService<Diagnosis>();
        }

        public void StartDiagnosis()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
