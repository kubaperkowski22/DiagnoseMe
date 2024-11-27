using ControlzEx.Standard;
using DiagnoseMe.Helpers;
using DiagnoseMe.Models;
using DiagnoseMe.Tools;
using DiagnoseMe.Tools.Data;
using DiagnoseMe.Tools.Diagnose;
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
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace DiagnoseMe.ViewModels
{
    public class DiagnoseVM : INotifyPropertyChanged
    {
        private AppDbContext _db;
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

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
                OnPropertyChanged(nameof(IsResultReady));
            }
        }
        private string _result;

        public string Disease
        {
            get => _disease;
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Disease));
            }
        }
        private string _disease;

        public bool IsPrimaryDataFilled
        {
            get => !string.IsNullOrEmpty(Name);
        }

        public bool IsResultReady
        {
            get => !string.IsNullOrEmpty(Result);
        }

        public DiagnoseVM()
        {
            _db = App.Current.Services.GetService<AppDbContext>();
            Diagnosis = App.Current.Services.GetService<Diagnosis>();
            ChatGpt = new ChatGptService();

            Name = string.Empty;
            YearOfBirth = 1999;
            Weight = 60;
            Height = 160;
            Result = string.Empty;

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
            //MessageBox.Show(message);

            Result = string.Empty;
           // Result = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla nec accumsan ligula, nec consectetur est. Aenean mattis eros eu augue rhoncus hendrerit. Nullam iaculis, arcu id posuere consectetur, massa neque rutrum elit, id pharetra lorem enim id magna. Vestibulum vehicula tellus tellus, ut maximus diam sollicitudin sed. Proin odio erat, cursus eu mi nec, elementum faucibus metus. Ut vestibulum erat ex, vel dignissim odio blandit quis. Suspendisse consectetur felis non ultricies tempor. Curabitur volutpat volutpat viverra. Proin imperdiet est nibh, ut ullamcorper tellus ullamcorper ut. Etiam id elementum libero. Aenean venenatis gravida nunc et cursus. Aliquam dictum eu ex ac iaculis. Quisque ultrices tristique ligula sit amet consequat. In euismod erat id rhoncus laoreet. Phasellus molestie lorem id semper rhoncus. Sed pharetra tellus erat, eget hendrerit tellus tempor sed.\r\n\r\nClass aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam id maximus sapien. Mauris vitae neque lacinia, cursus massa at, aliquam mauris. Vivamus vitae quam lectus. Pellentesque porttitor purus lectus. Cras accumsan blandit est, et sollicitudin nibh efficitur quis. Morbi vitae nisi vel ex dapibus maximus ut in justo. Mauris sodales ante lacus. In justo neque, fringilla eget luctus nec, mattis ut neque. Duis vestibulum eget urna quis tristique. Aenean vel cursus ipsum, id aliquet lacus. Nam tortor nisi, placerat non pellentesque quis, rhoncus nec magna. Mauris lacinia, mi sed bibendum aliquet, felis dolor vulputate metus, porta dapibus lacus elit vitae metus. Sed interdum eros ligula, sit amet laoreet tortor iaculis posuere.\r\n\r\nPhasellus fermentum, eros quis varius efficitur, enim erat mollis ipsum, ac malesuada dolor tellus eget tortor. Nulla turpis quam, dignissim sit amet convallis sit amet, condimentum ac libero. Vestibulum eget neque maximus, tincidunt est a, rhoncus purus. Duis quis metus iaculis, congue massa vel, tempus ex. Nulla finibus non augue a fringilla. Etiam aliquam elit ac mauris varius, eget dapibus lectus aliquet. Ut ornare vestibulum pellentesque.";
            Result = await ChatGpt.SendMessageAsync(message);
           // MessageBox.Show(Result, "Odpowiedź");

            var mainWindowVM = App.Current.Services.GetService<MainWindowVM>();
            if(mainWindowVM.IsUserLoggedIn)
            {
                var diagnosisResult = GetDiagnosisResultObject(mainWindowVM.LoggedUser.Id);
                _db.DiagnosisResults.Add(diagnosisResult);

                await _db.SaveChangesAsync();
            }
        }

        public string CreateMessageForChatGpt()
        {
            string primaryInfoMessage = $"Przeprowadzono wywiad medyczny z pacjentem. Pacjent to {GetEnumDescription(Gender)} o imieniu {Name}. " +
                                        $"Został urodzony w roku {YearOfBirth}. Ma {Height}cm wzrostu i {Weight}kg wagi.\n\n";
            string symptomsMessage = "Pacjent podał jakie objawy u niego występują i odpowiedział na kilka pytań dotyczących tych objawów. Oto one:\n";

            foreach (var item in SymptomsQuestions)
            {
                if (string.IsNullOrEmpty(item.Answer)) continue;

                symptomsMessage += "Objaw : " + Diagnosis.SymptomsDictionary.First(x => x.Key == item.Key).Value + "\n";
                symptomsMessage += "Pytanie : " + item.Question + "\n";
                symptomsMessage += "Odpowiedź pacjenta: " + item.Answer + "\n\n";
            }

            string ending = "Na podstawie wyżej przedstawionych danych zdiagnozuj pacjenta i przedstaw mu zalecenia do postawionej diagnozy. " +
                            "Nie podawaj kilku możliwości chorób, tylko jedną najbardziej prawdopodobną.\n" +
                            "Nazwę choroby przedstaw w pierwszej linii. W kolejnym akapicie linii napisz krótki opis, a w ostatnim akapicie wypunktuj zalecenia." +
                            "Nie pisz nic na tematy niedotyczące tej diagnozy.";

            return primaryInfoMessage + symptomsMessage + ending;
        }

        private DiagnosisResult GetDiagnosisResultObject(int userId)
        {
            DiagnosisResult diagnosisResult = new();

            if (string.IsNullOrEmpty(Result))
                return null;

            var paragraphs = Result.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            var cleanedParagraphs = new List<string>();
            for(int i = 0; i<paragraphs.Length; ++i)
            {
                paragraphs[i].Trim();
            }

            diagnosisResult.UserId = userId;
            diagnosisResult.DiseaseName = paragraphs[0];
            diagnosisResult.Description = paragraphs[1];
            diagnosisResult.Recommendations = paragraphs[2];
            diagnosisResult.DateOnly = DateOnly.FromDateTime(DateTime.Now);

            return diagnosisResult;
        }

        public bool CheckIfAllQuestionsAnswered()
        {
            if (SymptomsQuestions.Any(x => string.IsNullOrEmpty(x.Answer) == true))
                return false;
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
