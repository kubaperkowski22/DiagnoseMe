using DiagnoseMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        private EPageState _pageState;
        private enum EPageState
        {
            PrimaryData = 1,
            SymptomsSelection = 2,
            AdditionalQuestions = 3,
            Result = 4
        }

        public DiagnoseView()
        {
            InitializeComponent();

            ViewModel = new DiagnoseVM();
            this.DataContext = ViewModel;

            _pageState = EPageState.PrimaryData;
            BirthYear_PickerControl.Maximum = DateTime.UtcNow.Year;
        }

        private void StartDiagnosis_Button_Click(object sender, RoutedEventArgs e)
        {
            PageTitle_TextBlock.Text = "Uzupełnij dane";
            StartDiagnosis_Button.Visibility = Visibility.Collapsed;
            PrimaryData_Grid.Visibility = Visibility.Visible;
        }

        private void NextPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            _pageState += 1;
            SetView();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageState -= 1;
            SetView();
        }

        private void SetView()
        {
            if(_pageState == EPageState.AdditionalQuestions)
                ViewModel.UpdateQuestionList();

            switch (_pageState)
            {
                case EPageState.PrimaryData:
                    PageTitle_TextBlock.Text = "Uzupełnij dane";
                    StartDiagnosis_Button.Visibility = Visibility.Collapsed;
                    PrimaryData_Grid.Visibility = Visibility.Visible;
                    Sypmthoms_Grid.Visibility = Visibility.Collapsed;
                    AdditionalQuestions_Grid.Visibility = Visibility.Collapsed;
                    return;
                case EPageState.SymptomsSelection:
                    PageTitle_TextBlock.Text = "Wybierz objawy";
                    StartDiagnosis_Button.Visibility = Visibility.Collapsed;
                    PrimaryData_Grid.Visibility = Visibility.Collapsed;
                    Sypmthoms_Grid.Visibility = Visibility.Visible;
                    AdditionalQuestions_Grid.Visibility = Visibility.Collapsed;
                    return;
                case EPageState.AdditionalQuestions:
                    PageTitle_TextBlock.Text = "Dodatkowe pytania";
                    StartDiagnosis_Button.Visibility = Visibility.Collapsed;
                    PrimaryData_Grid.Visibility = Visibility.Collapsed;
                    Sypmthoms_Grid.Visibility = Visibility.Collapsed;
                    AdditionalQuestions_Grid.Visibility = Visibility.Visible;
                    return;
                case EPageState.Result:
                    PageTitle_TextBlock.Text = "Wynik";
                    StartDiagnosis_Button.Visibility = Visibility.Collapsed;
                    PrimaryData_Grid.Visibility = Visibility.Collapsed;
                    Sypmthoms_Grid.Visibility = Visibility.Collapsed;
                    AdditionalQuestions_Grid.Visibility = Visibility.Collapsed;
                    return;
                default:
                    return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hejka = string.Empty;

            foreach(var item in ViewModel.SymptomsButtonsStates)
            {
                if (item.Value == true)
                    hejka += item.Key + '\n';
            }

            MessageBox.Show(hejka, "zaznaczone objawy");
        }
    }
}
