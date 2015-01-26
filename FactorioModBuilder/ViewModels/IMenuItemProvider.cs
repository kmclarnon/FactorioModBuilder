using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels
{
    /// <summary>
    /// Provides the basic interface necessary to generate a dynamic context menu item
    /// </summary>
    public interface IMenuItemProvider
    {
        /// <summary>
        /// The text to be displayed on the menu
        /// </summary>
        string Header { get; }

        /// <summary>
        /// The command binding for any action to be taken on click
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// Collection containing all menu items that are children of this menu
        /// </summary>
        IEnumerable<IMenuItemProvider> SubMenus { get; }

        /// <summary>
        /// Whether or not this menu item can be checked
        /// </summary>
        bool IsCheckable { get; }

        /// <summary>
        /// Whether or not this menu item is checked
        /// </summary>
        bool IsChecked { get; set; }
        
        /// <summary>
        /// Whether or not this menu item is currently visible to the user
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Whether or not this menu item is a separator
        /// </summary>
        bool IsSeparator { get; }

        /// <summary>
        /// The icon to display next to this menu item header
        /// </summary>
        object Icon { get; }

        /// <summary>
        /// The tool tip to display for this context menu
        /// </summary>
        string ToolTip { get; }
    }
}
