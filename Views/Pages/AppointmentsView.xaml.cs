using DiagnoseMe.ViewModels;
using DiagnoseMe.Views.Windows;
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
using Microsoft.Extensions.DependencyInjection;
using DiagnoseMe.Models;

namespace DiagnoseMe.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsView : UserControl
    {
        public AppointmentsVM ViewModel
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
        private AppointmentsVM _viewModel;
        public AppointmentsView()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetService<AppointmentsVM>();
            this.DataContext = ViewModel;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow()
            {
                Owner = Window.GetWindow(this),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            addEventWindow.ShowDialog();
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            var eventToDelete = Appointments_DataGrid.SelectedItem as Appointment;

            if(eventToDelete != null)
                ViewModel.Appointments.Remove(eventToDelete);
        }
    }
}
