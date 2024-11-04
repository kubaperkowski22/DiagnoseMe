using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace DiagnoseMe.Tools
{
    public class Diagnosis
    {
        public Dictionary<string, string> SymptomsDictionary { get; set; }
        public Dictionary<string, List<string>> SymptomsQuestions { get; set; }

        public Diagnosis()
        {
            SetSymptomsDictionary();
            SetSymptomsQuestionsDictionary();
        }

        private void SetSymptomsDictionary()
        {
#if DEBUG
            string symptomsFilePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Resources\\JSON", "Symptoms_PL.json");
#else
            string symptomsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\JSON", "Symptoms_PL.json");
#endif

            if (File.Exists(symptomsFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(symptomsFilePath);

                    SymptomsDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas odczytu pliku z chorobami: {ex.Message}", "Błąd");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Plik Symptoms_PL.json nie został znaleziony w folderze Resources.", "Błąd");
                return;
            }
        }

        private void SetSymptomsQuestionsDictionary()
        {
#if DEBUG
            string questionsFilePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Resources\\JSON", "SymptomsQuestions_PL.json");
#else
            string questionsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\JSON", "SymptomsQuestions_PL.json");
#endif

            if (File.Exists(questionsFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(questionsFilePath);

                    SymptomsQuestions = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas odczytu pliku z pytaniami: {ex.Message}", "Błąd");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Plik SymptomsQuestions_PL.json nie został znaleziony w folderze Resources.", "Błąd");
                return;
            }
        }

    }
}
