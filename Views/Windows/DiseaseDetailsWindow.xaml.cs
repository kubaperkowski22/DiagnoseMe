using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiagnoseMe.Models;
using DiagnoseMe.Tools.Data;
using DiagnoseMe.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnoseMe.Views.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy DiseaseDetailsWindow.xaml
    /// </summary>
    public partial class DiseaseDetailsWindow : MetroWindow
    {
        private DiagnosisResult DiagnosisResult { get; set; }
        public DiseaseDetailsWindow(DiagnosisResult diagnosisResult)
        {
            InitializeComponent();

            DiagnosisResult = diagnosisResult;

            DiseaseName_TextBlock.Text = DiagnosisResult.DiseaseName;
            Date_TextBlock.Text = DiagnosisResult.DateOnly.ToString();
            Description_TextBox.Text = DiagnosisResult.Description;
            Recommendations_TextBlock.Text = DiagnosisResult.Recommendations;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var historyVM = App.Current.Services.GetService<HistoryVM>();

            historyVM.DeleteDiagnosisFromHistory(DiagnosisResult);

            CloseWindow();
        }
    }
}
