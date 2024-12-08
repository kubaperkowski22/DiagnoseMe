using System;
using System.Windows.Markup;

namespace DiagnoseMe.Tools
{
    public class TranslateExtension : MarkupExtension
    {
        public string Key { get; set; }

        public TranslateExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LocalizationManager.Instance[Key];
        }
    }
}
