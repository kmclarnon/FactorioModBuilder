using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.Menu
{
    /// <summary>
    /// Provides a basic checkable menu item
    /// </summary>
    public class CheckableItem : MenuItemProvider
    {
        /// <summary>
        /// The action to execute when this item is checked
        /// </summary>
        private Action _onChecked;

        /// <summary>
        /// Creates a basic checkable menu item that is unchecked
        /// </summary>
        /// <param name="header">The text to display</param>
        public CheckableItem(string header, Action onChecked)
            : this(header, onChecked, false)
        {
            this.IsCheckable = true;
            _onChecked = onChecked;
        }

        /// <summary>
        /// Creates a basic checkable menu item that is optionally checked
        /// </summary>
        /// <param name="header">The text to display</param>
        /// <param name="onChecked">The action to perform when this item is checked</param>
        /// <param name="isChecked">Whether or not this item should start off checked</param>
        /// <remarks>
        /// Even if the isChecked parameter is set to true, the onChecked action will
        /// only be executed if the item is unchecked and then checked again
        /// </remarks>
        public CheckableItem(string header, Action onChecked, bool isChecked)
            : this(header, onChecked, isChecked, null)
        {
        }

        /// <summary>
        /// Creates a basic checkable menu item that is optionally checked
        /// </summary>
        /// <param name="header">The text to display</param>
        /// <param name="onChecked">The action to perform when this item is checked</param>
        /// <param name="isChecked">Whether or not this item should start off checked</param>
        /// <param name="icon">The icon to display with this menu item</param>
        /// <remarks>
        /// Even if the isChecked parameter is set to true, the onChecked action will
        /// only be executed if the item is unchecked and then checked again
        /// </remarks>
        public CheckableItem(string header, Action onChecked, bool isChecked, object icon)
            : base(header)
        {
            this.IsCheckable = true;
            this.IsChecked = isChecked;
            if (onChecked == null)
                throw new ArgumentNullException("onChecked");
            _onChecked = onChecked;
            this.Icon = icon;
        }

        /// <summary>
        /// Executes the action provided to the constructor when this menu item is checked
        /// </summary>
        protected override void OnIsChecked()
        {
            if (_onChecked != null)
                _onChecked();
        }
    }
}
