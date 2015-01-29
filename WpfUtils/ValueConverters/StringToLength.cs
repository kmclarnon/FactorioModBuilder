﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Returns the length of the string generated by the values ToString method
    /// </summary>
    [ValueConversion(typeof(object), typeof(int))]
    public class StringToLength : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return 0;
            else
                return value.ToString().Length;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}