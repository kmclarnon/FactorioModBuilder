using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides a converter that returns false if the object is null
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class ObjectNotNull : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
