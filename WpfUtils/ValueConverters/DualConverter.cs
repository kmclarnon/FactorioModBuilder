using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides an interface to chain two separate value converters together
    /// </summary>
    public class DualConverter : BaseConverter
    {
        public IValueConverter Stage1 { get; set; }
        public IValueConverter Stage2 { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Stage2.Convert(
                Stage1.Convert(
                    value, targetType, parameter, culture), 
                targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Stage1.ConvertBack(
                Stage2.ConvertBack(
                    value, targetType, parameter, culture), 
                targetType, parameter, culture);
        }
    }
}
