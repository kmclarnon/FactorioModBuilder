using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfUtils.Extensions;

namespace WpfUtils.ValueConverters
{
    /// <summary>
    /// Determines the left margin based on the indent legth and the TreeViewItem's depth
    /// </summary>
    public class LeftMarginMultiplier : BaseConverter
    {
        /// <summary>
        /// The length per indent
        /// </summary>
        public double Length { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);
            return new Thickness(this.Length * item.GetDepth(), 0, 0, 0);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
