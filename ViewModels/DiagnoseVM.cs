using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiagnoseMe.ViewModels
{
    public class DiagnoseVM : INotifyPropertyChanged
    {
        public Dictionary<string, string> SymptomsDictionary
        {
            get => _symptomsDictionary;
            set
            {
                _symptomsDictionary = value;
                OnPropertyChanged();
            }
        }
        private Dictionary<string, string> _symptomsDictionary;

        public DiagnoseVM()
        {
            SetDictionary();
        }
        private void SetDictionary()
        {
            SymptomsDictionary = new Dictionary<string, string>
            {
            // Objawy ogólne
            { "fever", "Gorączka" },
            { "fatigue", "Zmęczenie" },
            { "musclePain", "Bóle mięśniowe" },
            { "chills", "Dreszcze" },
            { "nightSweats", "Nocne poty" },
            { "dizziness", "Zawroty głowy" },
            { "lossOfAppetite", "Brak apetytu" },

            // Objawy układu oddechowego
            { "cough", "Kaszel" },
            { "runnyNose", "Katar" },
            { "soreThroat", "Ból gardła" },
            { "shortnessOfBreath", "Duszność" },
            { "wheezing", "Świszczący oddech" },

            // Objawy układu pokarmowego
            { "abdominalPain", "Ból brzucha" },
            { "diarrhea", "Biegunka" },
            { "vomiting", "Wymioty" },
            { "heartburn", "Zgaga" },
            { "bloating", "Wzdęcia" },

            // Objawy dermatologiczne
            { "rash", "Wysypka" },
            { "itching", "Swędzenie skóry" },
            { "skinChanges", "Zmiany na skórze" },
            { "erythema", "Rumień" },

            // Objawy układu moczowego
            { "painOnUrination", "Ból przy oddawaniu moczu" },
            { "frequentUrination", "Częstomocz" },
            { "bloodInUrine", "Krew w moczu" },

            // Objawy układu krążenia
            { "chestPain", "Ból w klatce piersiowej" },
            { "palpitations", "Kołatanie serca" },
            { "legSwelling", "Obrzęki nóg" },

            // Objawy neurologiczne
            { "headache", "Bóle głowy" },
            { "tremor", "Drżenie" },
            { "speechDisturbance", "Zaburzenia mowy" },
            { "limbWeakness", "Niedowład kończyn" },
            { "lossOfConsciousness", "Utrata przytomności" }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
