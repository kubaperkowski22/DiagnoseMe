using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace DiagnoseMe.Tools
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager _instance;
        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private ResourceManager _resourceManager =
            new ResourceManager("DiagnoseMe.Resources.Strings.Strings", typeof(LocalizationManager).Assembly);

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string key] => _resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);
        public string GetString(string key)
        {
            return _resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);
        }
        public void ChangeLanguage(string cultureCode)
        {
            Debug.WriteLine("\n\n" + GetString("@diagnose_yourself"));

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            Debug.WriteLine("CurrentUICulture: " + Thread.CurrentThread.CurrentUICulture);
            Debug.WriteLine(GetString("@diagnose_yourself" + "\n\n"));

            foreach (Window window in App.Current.Windows)
            {
                var context = window.DataContext;
                window.DataContext = null;
                window.DataContext = context;
            }

            NotifyPropertyChanged(string.Empty);
        }
    }
}
