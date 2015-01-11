using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Value converter that negates a provided boolean value
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class BooleanNot : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}
