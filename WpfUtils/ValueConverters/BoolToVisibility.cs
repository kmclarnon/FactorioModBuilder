using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides a conversion between boolean and visibility values
    /// </summary>
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class BoolToVisibility : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
