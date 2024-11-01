using DiagnoseMe.ViewModels;
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
    /// Logika interakcji dla klasy DiagnosePage.xaml
    /// </summary>
    public partial class DiagnoseView : UserControl
    {
        public DiagnoseVM ViewModel
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
        private DiagnoseVM _viewModel;

        public DiagnoseView()
        {
            InitializeComponent();

            ViewModel = new DiagnoseVM();
            this.DataContext = ViewModel;

            BirthYear_PickerControl.Maximum = DateTime.UtcNow.Year;
        }

        private void StartDiagnosis_Button_Click(object sender, RoutedEventArgs e)
        {
            PageTitle_TextBlock.Text = "Uzupełnij dane";
            StartDiagnosis_Button.Visibility = Visibility.Collapsed;
            PrimaryData_Grid.Visibility = Visibility.Visible;

            ViewModel.StartDiagnosis();
        }

        private void NextPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            PageTitle_TextBlock.Text = "Wybierz objawy";
            StartDiagnosis_Button.Visibility = Visibility.Collapsed;
            PrimaryData_Grid.Visibility = Visibility.Collapsed;
            Sypmthoms_Grid.Visibility = Visibility.Visible;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitle_TextBlock.Text = "Uzupełnij dane";
            StartDiagnosis_Button.Visibility = Visibility.Collapsed;
            PrimaryData_Grid.Visibility = Visibility.Visible;
            Sypmthoms_Grid.Visibility = Visibility.Collapsed;
        }
    }
}
