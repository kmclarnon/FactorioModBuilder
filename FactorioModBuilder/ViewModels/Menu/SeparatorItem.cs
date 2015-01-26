using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.Menu
{
    /// <summary>
    /// Provides a simple separator in a menu
    /// </summary>
    public class SeparatorItem : MenuItemProvider
    {
        /// <summary>
        /// Creates a separator MenuItemProvider
        /// </summary>
        public SeparatorItem()
            : base(String.Empty)
        {
            this.IsSeparator = true;
        }
    }
}
