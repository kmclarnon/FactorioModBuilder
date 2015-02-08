using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Provides a method for string concatenation in the value converter
    /// </summary>
    public class StringConcat : BaseConverter
    {
        /// <summary>
        /// String to be prefixed to the provided value
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// String to be posfixed to the provided value
        /// </summary>
        public string Postfix { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var res = value as string;
            if(res != null)
            {
                if (this.Prefix != null && this.Prefix != String.Empty)
                    res = this.Prefix + res;
                if (this.Postfix != null && this.Postfix != String.Empty)
                    res = res + this.Postfix;
                return res;
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
