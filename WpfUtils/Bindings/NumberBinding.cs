using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUtils.Bindings
{
    public class NumberBinding : BaseBinding
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object val = base.ProvideValue(serviceProvider);

            DependencyObject obj;
            DependencyProperty prop;
            if(this.TryGetTarget(serviceProvider, out obj, out prop))
            {

            }

            return val;
        }
    }
}
