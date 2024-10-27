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
    /// Logika interakcji dla klasy FindDoctorsView.xaml
    /// </summary>
    public partial class FindDoctorsView : UserControl
    {
        public FindDoctorsView()
        {
            InitializeComponent();
            MyWebBrowser.Navigate("https://gsl.nfz.gov.pl/GSL/GSL/PrzychodnieSpecjalistyczne");
            MyWebBrowser2.Navigate("https://gsl.nfz.gov.pl/GSL/GSL/PrzychodnieSpecjalistyczne");
        }
    }
}
