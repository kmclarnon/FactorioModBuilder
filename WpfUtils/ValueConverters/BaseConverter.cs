using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides a base converter class that can be used with the
    /// binding syntax Converter={x:DerivedConverter}
    /// </summary>
    public abstract class BaseConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
    }
}
