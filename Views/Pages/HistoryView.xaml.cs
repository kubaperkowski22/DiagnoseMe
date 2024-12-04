using DiagnoseMe.Models;
using DiagnoseMe.ViewModels;
using DiagnoseMe.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagnoseMe.Views
{
    /// <summary>
    /// Logika interakcji dla klasy HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryVM ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
            }
        }
        private HistoryVM _viewModel;
        public HistoryView()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetService<HistoryVM>();
            this.DataContext = ViewModel;
        }

        private void ShowDetails_ButtonClick(object sender, RoutedEventArgs e)
        {
            var diagnosisResult = (sender as Button).DataContext as DiagnosisResult; // Typ danych powiązanych z DataTemplate
            if (diagnosisResult != null)
            {
                ViewModel.OpenDetailsWindow(diagnosisResult);
            }
        }
    }
}
