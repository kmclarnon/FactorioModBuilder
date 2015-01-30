using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.Menu
{
    /// <summary>
    /// Provides a simple menu item that optionally executes a command when clicked
    /// </summary>
    public class ClickableItem : MenuItemProvider
    {
        /// <summary>
        /// The action to execute when this item is clicked
        /// </summary>
        private Action _onClick;

        /// <summary>
        /// The function used to determine if _onClick can be executed
        /// </summary>
        private Func<bool> _canClick;

        /// <summary>
        /// Creates a menu item that can optionally execute a command when clicked on by the user
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="onClick">The action to execute when this item is clicked</param>
        public ClickableItem(string header, Action onClick)
            : this(header, onClick, (() => true))
        {
        }

        /// <summary>
        /// Creates a menu item that can optionally execute a command when clicked on by the user
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="onClick">The action to execute when this item is clicked</param>
        /// <param name="canClick">The function used to determine if this menu item can currently be clicked on</param>
        public ClickableItem(string header, Action onClick, Func<bool> canClick)
            : this(header, onClick, canClick, null)
        {
        }

        /// <summary>
        /// Creates a menu item that can optionally execute a command when clicked on by the user
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="onClick">The action to execute when this item is clicked</param>
        /// <param name="canClick">The function used to determine if this menu item can currently be clicked on</param>
        /// <param name="icon">The icon to display next to this menu item</param>
        public ClickableItem(string header, Action onClick, Func<bool> canClick, object icon)
            : this(header, onClick, canClick, icon, String.Empty)
        {
        }

        /// <summary>
        /// Creates a menu item that can optionally execute a command when clicked on by the user
        /// </summary>
        /// <param name="header">The text to be displayed on this menu item</param>
        /// <param name="onClick">The action to execute when this item is clicked</param>
        /// <param name="canClick">The function used to determine if this menu item can currently be clicked on</param>
        /// <param name="icon">The icon to display next to this menu item</param>
        /// <param name="toolTip">The tool tip to be displayed when the mouse hovers over this menu item</param>
        public ClickableItem(string header, Action onClick, Func<bool> canClick, object icon, string toolTip)
            : base(header)
        {
            if (onClick == null)
                throw new ArgumentNullException("onClick");
            if (canClick == null)
                throw new ArgumentNullException("canClick");
            _onClick = onClick;
            _canClick = canClick;
            this.Icon = icon;
            this.ToolTip = toolTip;
        }

        /// <summary>
        /// Determines if the menu item command binding can be executed
        /// </summary>
        /// <returns>The result of _canClick</returns>
        protected override bool CanExecuteCommand()
        {
            return _canClick();
        }

        /// <summary>
        /// Command binding of the menu item
        /// </summary>
        protected override void ExecuteCommand()
        {
            _onClick();
        }
    }
}
