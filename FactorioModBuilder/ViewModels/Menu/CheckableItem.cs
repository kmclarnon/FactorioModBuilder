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
        private Action<bool> _checkChanged;

        /// <summary>
        /// Creates a basic checkable menu item that is unchecked
        /// </summary>
        /// <param name="header">The text to display</param>
        /// <param name="checkChanged">The action to perform when the checked state changes</param>
        public CheckableItem(string header, Action<bool> checkChanged)
            : this(header, checkChanged, false)
        {
        }

        /// <summary>
        /// Creates a basic checkable menu item that is optionally checked
        /// </summary>
        /// <param name="header">The text to display</param>
        /// <param name="onChecked">The action to perform when this item is checked</param>
        /// <param name="checkChanged">The action to perform when the checked state changes</param>
        /// <remarks>
        /// Even if the isChecked parameter is set to true, the onChecked action will
        /// only be executed if the item is unchecked and then checked again
        /// </remarks>
        public CheckableItem(string header, Action<bool> checkChanged, bool isChecked)
            : this(header, checkChanged, isChecked, null)
        {
        }

        /// <summary>
        /// Creates a basic checkable menu item that is optionally checked
        /// </summary>
        /// <param name="header">The text to display</param>
        /// <param name="onIsCheckedChanged">The action to perform when the checked state changes</param>
        /// <param name="isChecked">Whether or not this item should start off checked</param>
        /// <param name="icon">The icon to display with this menu item</param>
        /// <remarks>
        /// Even if the isChecked parameter is set to true, the onChecked action will
        /// only be executed if the item is unchecked and then checked again
        /// </remarks>
        public CheckableItem(string header, Action<bool> checkChanged, bool isChecked, object icon)
            : base(header)
        {
            this.IsCheckable = true;
            this.IsChecked = isChecked;
            if (checkChanged == null)
                throw new ArgumentNullException("onChecked");
            _checkChanged = checkChanged;
            this.Icon = icon;
        }

        /// <summary>
        /// Executes the checkChanged action provided by the user
        /// </summary>
        /// <param name="oldVal">The old value of IsChecked</param>
        /// <param name="newVal">The new value of IsChecked</param>
        protected override void OnIsCheckedChanged(bool oldVal, bool newVal)
        {
            if(_checkChanged != null)
                _checkChanged(newVal);
        }
    }
}
