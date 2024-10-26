using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using DiagnoseMe.Views;
using DiagnoseMe.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnoseMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindowVM ViewModel
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
        private MainWindowVM _viewModel;

        private AppointmentsView _appointmentsView;
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetService<MainWindowVM>();
            this.DataContext = ViewModel;

            _appointmentsView = App.Current.Services.GetService<AppointmentsView>() as AppointmentsView;
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as HamburgerMenuIconItem;
            if (menuItem is not null)
            {
                if (menuItem.Tag.ToString() == "Diagnosis")
                {
                    HamburgerMenuControl.Content = new Views.DiagnoseView();
                    this.HamburgerMenuControl.IsPaneOpen = false;
                    return;
                }

                if (!ViewModel.IsUserLoggedIn)
                {
                    RedirectToLogInView();
                    return;
                }

                switch (menuItem.Tag.ToString())
                {
                    case "FindDoctor":
                        HamburgerMenuControl.Content = new Views.FindDoctorsView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Calendar":
                        HamburgerMenuControl.Content = _appointmentsView;
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "History":
                        HamburgerMenuControl.Content = new Views.HistoryView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Notifications":
                        HamburgerMenuControl.Content = new Views.NotificationsView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    default:
                        return;
                }
            }
        }

        private void HamburgerMenuControl_OptionsItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as HamburgerMenuIconItem;
            if (menuItem is not null)
            {
                switch (menuItem.Tag.ToString())
                {
                    case "Account":
                        HamburgerMenuControl.Content = new Views.AccountView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Settings":
                        HamburgerMenuControl.Content = new Views.SettingsView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    default:
                        return;
                }
            }
        }

        private void RedirectToLogInView()
        {
            HamburgerMenuControl.Content = new Views.LogInView();
            this.HamburgerMenuControl.IsPaneOpen = false;
        }

    }
}