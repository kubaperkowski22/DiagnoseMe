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
using DiagnoseMe.Views.Windows;

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
        private HistoryView _historyView;
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = App.Current.Services.GetService<MainWindowVM>();
            this.DataContext = ViewModel;

            _appointmentsView = App.Current.Services.GetService<AppointmentsView>();
            _historyView = App.Current.Services.GetService<HistoryView>();
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as HamburgerMenuIconItem;
            if (menuItem is not null)
            {
                if (menuItem.Tag.ToString() == "Diagnosis")
                {
                    HamburgerMenuControl.Content = new DiagnoseView();
                    this.HamburgerMenuControl.IsPaneOpen = false;
                    return;
                }

                if (!ViewModel.IsUserLoggedIn)
                {
                    OpenLogInWindow();
                    return;
                }

                switch (menuItem.Tag.ToString())
                {
                    case "FindDoctor":
                        HamburgerMenuControl.Content = new FindDoctorsView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Calendar":
                        HamburgerMenuControl.Content = _appointmentsView;
                        _appointmentsView.ViewModel.RefreshAppointments();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "History":
                        HamburgerMenuControl.Content = _historyView;
                        _historyView.ViewModel.RefresHistory();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Notifications":
                        HamburgerMenuControl.Content = new NotificationsView();
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
                        if (!ViewModel.IsUserLoggedIn)
                        {
                            OpenLogInWindow();
                            break;
                        }
                        HamburgerMenuControl.Content = new AccountView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    case "Settings":
                        HamburgerMenuControl.Content = new SettingsView();
                        this.HamburgerMenuControl.IsPaneOpen = false;
                        break;
                    default:
                        return;
                }
            }
        }

        private void OpenLogInWindow()
        {
            this.HamburgerMenuControl.IsPaneOpen = false;

            var logInWindow = new LogInRegisterWindow();
            logInWindow.Owner = this;
            logInWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            logInWindow.ShowDialog();
        }
    }
}