using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfUtils.ValueConverters
{
    public abstract class BaseConverter : MarkupExtension
    /// <summary>
    /// Provides a base converter class that can be used with the
    /// binding syntax Converter={x:DerivedConverter}
    /// </summary>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
