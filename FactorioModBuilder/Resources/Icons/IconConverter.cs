using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils.ValueConverters;

namespace FactorioModBuilder.Resources.Icons
{
    public class IconConverter : BaseConverter
    {
        public Object IconNoIcon { get; set; }
        public Object IconFilterOpen { get; set; }
        public Object IconFilterClosed { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value != null && value is AppIcon)
            {
                switch ((AppIcon)value)
                {
                    case AppIcon.NoIcon: return this.IconNoIcon;
                    case AppIcon.FilterClosed: return this.IconFilterClosed;
                    case AppIcon.FilterOpen: return this.IconFilterOpen;
                    default:
                        throw new Exception("Unknown AppIcon enumeration value");
                }
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
