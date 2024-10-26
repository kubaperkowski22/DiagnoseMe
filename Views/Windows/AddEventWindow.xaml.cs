using DiagnoseMe.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using DiagnoseMe.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DiagnoseMe.Views.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : MetroWindow
    {
        public AddEventWindow()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string eventName = EventName_TextBox.Text;
            DateTime dateTime = (DateTime)DateTimePickerControl.SelectedDateTime;
            string localisation = Localisation_TextBox.Text;

            var appointmentsVM = App.Current.Services.GetService<AppointmentsVM>() as AppointmentsVM;
            appointmentsVM.AddAppointment(new Appointment(eventName, dateTime, localisation));

            this.Close();
        }

        private void DateTimePickerControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DateTimePickerControl.IsDropDownOpen = true;
        }

        private void DateTimePickerControl_SelectedDateTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (e.NewValue < DateTime.UtcNow)
            {
                Add_Button.IsEnabled = false;
                DateTime_ValidationTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                DateTime_ValidationTextBlock.Visibility = Visibility.Hidden;
                DateTimePicker_TextBlock.Text = e.NewValue.ToString();

                if(!string.IsNullOrEmpty(EventName_TextBox.Text))
                    Add_Button.IsEnabled = true;
            }
                
        }

        private void EventName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EventName_TextBox.Text))
            {
                Add_Button.IsEnabled = false;
                EventName_ValidationWrapPanel.Visibility = Visibility.Visible;
            }
            else
            {
                EventName_ValidationWrapPanel.Visibility = Visibility.Hidden;

                if (DateTimePickerControl.SelectedDateTime > DateTime.UtcNow)
                    Add_Button.IsEnabled = true;
            }
        }
    }
}
