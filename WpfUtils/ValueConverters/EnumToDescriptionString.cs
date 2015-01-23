using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Converts an enum to a string using a description attribute if possible
    /// </summary>
    public class EnumToDescriptionString : BaseConverter
    {
        /// <summary>
        /// Converts an enum to a string
        /// </summary>
        /// <returns>The description tag of the enum if possible, otherwise the resutl of ToString()</returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;
            var res = value as Enum;
            return this.GetDescription(res);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the description attribute value of an enum if it exists, otherwise returns ToString()
        /// </summary>
        /// <param name="e">The enum to search for a description</param>
        /// <returns>The value of the description attribute, if it exists, otherwise ToString()</returns>
        private string GetDescription(Enum e)
        {
            if (e == null)
                return String.Empty;

            FieldInfo fInfo = e.GetType().GetField(e.ToString());
            object[] attribs = fInfo.GetCustomAttributes(false);

            if (attribs.Length == 0)
                return e.ToString();
            else
            {
                var res = attribs.Where(o => o is DescriptionAttribute);
                if (!res.Any())
                    return e.ToString();
                else
                    return ((DescriptionAttribute)res.First()).Description;
            }
        }
    }
}
