using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.Menu
{
    /// <summary>
    /// Provides a menu item that contains one or more sub menu items
    /// </summary>
    public class CategoryItem : MenuItemProvider
    {
        /// <summary>
        /// Creates a menu item that contains a one or more sub items
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="subItems">The sub items to be shown as children of this menu item</param>
        public CategoryItem(string header, params IMenuItemProvider[] subItems)
            : this(header, subItems.ToList())
        {
        }

        /// <summary>
        /// Creates a menu item that contains a one or more sub items
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="subItems">The sub items to be shown as children of this menu item</param>
        public CategoryItem(string header, IEnumerable<IMenuItemProvider> subItems)
            : this(header, null, subItems)
        {
        }

        /// <summary>
        /// Creates a menu item that contains a one or more sub items
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="icon">The icon to display next to this menu item</param>
        /// <param name="subItems">The sub items to be shown as children of this menu item</param>
        public CategoryItem(string header, object icon, IEnumerable<IMenuItemProvider> subItems)
            : base(header)
        {
            foreach (var s in subItems)
                this.SubItems.Add(s);
            this.Icon = icon;
        }
    }
}
