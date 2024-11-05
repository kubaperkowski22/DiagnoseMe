using DiagnoseMe.Helpers;
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
        public ObservableCollection<SymptomQuestion> SymptomsQuestions { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(IsPrimaryDataFilled));
            }
        }
        private string _name;

        public int YearOfBirth
        {
            get => _yearOfBirth;
            set
            {
                _yearOfBirth = value;
                OnPropertyChanged(nameof(YearOfBirth));
            }
        }
        private int _yearOfBirth;

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        private int _height;

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        private int _weight;

        public EGender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        private EGender _gender;

        public bool IsPrimaryDataFilled
        {
            get => !string.IsNullOrEmpty(Name);
        }

        public DiagnoseVM()
        {
            Diagnosis = App.Current.Services.GetService<Diagnosis>();

            Name = string.Empty;
            YearOfBirth = 1999;
            Weight = 60;
            Height = 160;

            SymptomsButtonsStates = new ObservableCollection<SymptomState>(Diagnosis.SymptomsDictionary.Select(x => new SymptomState { Key = x.Key, Value = false }));
            SymptomsQuestions = new ObservableCollection<SymptomQuestion>();
        }

        public void UpdateQuestionList()
        {
            SymptomsQuestions.Clear();
            var selectedSymptoms = GetSelectedSymptoms();

            foreach(var symptom in selectedSymptoms)
            {
                foreach (var question in Diagnosis.GetQuestions(symptom.Key))
                {
                    SymptomsQuestions.Add(new SymptomQuestion(symptom.Key, question));
                }
            }
        }

        public List<SymptomState> GetSelectedSymptoms()
        {
            var selectedSymptoms = new List<SymptomState>();
            foreach(var item in SymptomsButtonsStates)
            {
                if (item.Value)
                    selectedSymptoms.Add(item);
            }
            return selectedSymptoms;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
