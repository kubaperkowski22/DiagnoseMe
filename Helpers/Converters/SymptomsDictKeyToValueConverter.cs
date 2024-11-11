using DiagnoseMe.Tools.Diagnose;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DiagnoseMe.Helpers.Converters
{
    public class SymptomsDictKeyToValueConverter : IValueConverter
    {
        private Dictionary<string, string> SymptomsDictionary
        {
            get
            {
                if (_symptomsDictionary is null || _symptomsDictionary.Count == 0)
                {
                    var diagnosis = App.Current.Services.GetService<Diagnosis>();
                    _symptomsDictionary = diagnosis.SymptomsDictionary;
                }

                return _symptomsDictionary;
            }
        }
        private Dictionary<string, string> _symptomsDictionary;

        public object Convert(object key, Type targetType, object parameter, CultureInfo culture)
        {
            string value = SymptomsDictionary[key as string];

            if (value != null)
                return value;
            return key; // returns key if could not find value from dictionary
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
