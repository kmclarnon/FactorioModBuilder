using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Menu.Base
{
    /// <summary>
    /// THe base class implementation of IMenuItemProvider, the view model for menu items
    /// </summary>
    public class MenuItemProvider : BaseVM, IMenuItemProvider
    {
        /// <summary>
        /// The text to be displayed on the menu
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// The command binding for any action to be taken on click
        /// </summary>
        public ICommand Command { get { return this.GetCommand(this.ExecuteCommand, this.CanExecuteCommand); } }

        /// <summary>
        /// Collection containing all menu items that are children of this menu
        /// </summary>
        public ObservableCollection<IMenuItemProvider> SubItems { get; private set; }

        /// <summary>
        /// Whether or not this menu item can be checked
        /// </summary>
        public bool IsCheckable
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Whether or not this menu item is checked
        /// </summary>
        public bool IsChecked
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value, null, this.InternalIsCheckedChanged); }
        }

        /// <summary>
        /// Whether or not this menu item is currently visible to the user
        /// </summary>
        public bool IsVisible
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Whether or not this menu item is a separator
        /// </summary>
        public bool IsSeparator
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The icon to display next to this menu item header
        /// </summary>
        public object Icon
        {
            get { return this.GetProperty<object>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The tool tip to display for this context menu
        /// </summary>
        public string ToolTip
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Basic constructor. Creates a menu item with only header text
        /// </summary>
        public MenuItemProvider(string header)
        {
            this.Header = header;
            this.IsVisible = true;
            this.ToolTip = String.Empty;
            this.SubItems = new ObservableCollection<IMenuItemProvider>();
        }

        /// <summary>
        /// Determines whether or not this MenuItem execute command can be run
        /// </summary>
        /// <returns>True unless overridden</returns>
        protected virtual bool CanExecuteCommand()
        {
            return true;
        }

        /// <summary>
        /// The actions to take when this menu item command is invoked
        /// </summary>
        protected virtual void ExecuteCommand()
        {
        }

        /// <summary>
        /// Internal callback to handle virtual callback dispatch
        /// </summary>
        private void InternalIsCheckedChanged()
        {
            if(this.IsCheckable)
            {
                if (this.IsChecked)
                    this.OnIsChecked();
                this.OnIsCheckedChanged(!this.IsChecked, this.IsChecked);
            }
        }

        /// <summary>
        /// This callback is executed when the IsChecked property is set to true
        /// </summary>
        protected virtual void OnIsChecked()
        {
        }

        /// <summary>
        /// This callback is executed when the IsChecked 
        /// </summary>
        /// <param name="oldVal">The old value of IsChecked</param>
        /// <param name="newVal">The new value of IsChecked</param>
        protected virtual void OnIsCheckedChanged(bool oldVal, bool newVal)
        {
        }
    }
}
