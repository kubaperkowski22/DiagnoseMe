using DiagnoseMe.Tools;
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
        public ObservableCollection<SymptomState> SymptomsButtonsStates { get; set; }

        public Diagnosis Diagnosis { get; set; }

        public DiagnoseVM()
        {
            Diagnosis = App.Current.Services.GetService<Diagnosis>();

            SymptomsButtonsStates = new ObservableCollection<SymptomState>(Diagnosis.SymptomsDictionary.Select(x => new SymptomState { Key = x.Key, Value = false }));
        }

        public IEnumerable<string> GetSelectedKeys()
        {
            return SymptomsButtonsStates.Where(pair => pair.Value).Select(pair => pair.Key);
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
