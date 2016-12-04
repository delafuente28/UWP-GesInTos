using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UWPGesInTos.Converters
{
    /// <summary>
    /// CurrencyConverter
    /// </summary>
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is decimal)
            {
                decimal d = (decimal)value;
                CultureInfo cultureInfo = new CultureInfo("es-ES");

                return string.Format(cultureInfo, "{0:C}", d);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
