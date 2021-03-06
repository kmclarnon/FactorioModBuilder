﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides a convenience value converter to prevent a null image source from creating errors when bound to an Image
    /// </summary>
    public class NullImage : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
