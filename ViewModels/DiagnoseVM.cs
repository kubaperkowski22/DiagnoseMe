using DiagnoseMe.Helpers;
using DiagnoseMe.Tools;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiagnoseMe.ViewModels
{
    public class DiagnoseVM : INotifyPropertyChanged
    {
        public ObservableCollection<SymptomState> SymptomsButtonsStates { get; set; }
        public ObservableCollection<SymptomQuestion> SymptomsQuestions { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public ChatGptService ChatGpt { get; set; }
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
            ChatGpt = new ChatGptService();

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

        private string GetEnumDescription(Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return enumValue.ToString();
        }

        public async void GetDiagnosisResult()
        {
            string message = CreateMessageForChatGpt();
            MessageBox.Show(message);

            string response = await ChatGpt.GetChatGPTResponse(message);

            MessageBox.Show(response, "Odpowiedź");
        }

        public string CreateMessageForChatGpt()
        {
            string primaryInfoMessage = $"Przeprowadzono wywiad medyczny z pacjentem. Pacjent to {GetEnumDescription(Gender)} o imieniu {Name}. Został urodzony w roku {YearOfBirth}. Ma {Height}cm wzrostu i {Weight}kg wagi.\n\n";
            string symptomsMessage = "Pacjent podał jakie objawy u niego występują i odpowiedział na kilka pytań dotyczących tych objawów. Oto one:\n";

            foreach(var item in SymptomsQuestions)
            {
                if (string.IsNullOrEmpty(item.Answer)) continue;

                symptomsMessage += "Objaw : " + Diagnosis.SymptomsDictionary.First(x => x.Key == item.Key).Value + "\n";
                symptomsMessage += "Pytanie : " + item.Question + "\n";
                symptomsMessage += "Odpowiedź pacjenta: " + item.Answer + "\n\n";
            }

            string ending = "Na podstawie wyżej przedstawionych danych zdiagnozuj pacjenta i przedstaw mu zalecenia do postawionej diagnozy.";

            return primaryInfoMessage + symptomsMessage + ending;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
