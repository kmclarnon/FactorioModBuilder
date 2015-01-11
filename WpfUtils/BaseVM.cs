using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfUtils
{
    /// <summary>
    /// The view model base class
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Provides a convenient way to send notifications.  When called inside a property,
        /// it can be called safely with the default paramater
        /// </summary>
        /// <param name="property">The name of the property that is changing. This defaults to [CallerMemberName]</param>
        protected void NotifyPropertyChanged([CallerMemberName] string property = "no property")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    
        /// <summary>
        /// A convenience function for simplifying setters in derived classes.  Uses reflection to 
        /// determine appropriate properties to set and attempts to compare using IComparable&lt;T&lt;
        /// </summary>
        /// <param name="target">The model that contains the data backing the property</param>
        /// <param name="newValue">The value parameter of the setter</param>
        /// <param name="allowNull">Whether the property can be set to null or not</param>
        /// <param name="secondaryAction">Any action that should be performed after the property value is changed</param>
        /// <param name="targetProperty">The name of the property on the target object.  This must be filled out if the
        /// property on the model is not the same as on the view model</param>
        /// <param name="vmProperty">The name of the property that should be passed to NotifyPropertyChanged.  This must be
        /// filled out if this function is not called from the property itself</param>
        protected void SetProperty<T>(object target, T newValue, 
            bool allowNull = false, Action secondaryAction = null,
            [CallerMemberName] string targetProperty = "default property", 
            [CallerMemberName] string vmProperty = "view model property")
        {
            if (target == null)
                throw new ArgumentNullException("Target object cannot be null");

            bool changed = false;
            var tType = target.GetType();
            // get our target property's value to work with
            var tProp = tType.GetProperty(targetProperty);
            var tValue = tProp.GetValue(target);
            if (tValue.GetType() != typeof(T))
                throw new ArgumentException("Referenced property is not of type: " + typeof(T).FullName);
            if (!allowNull && newValue == null)
                return;
            // prefer using the comparable interface
            var tValComp = tValue as IComparable<T>;
            if (tValComp != null)
            {
                if (tValComp.CompareTo(newValue) != 0)
                {
                    tProp.SetValue(target, newValue);
                    changed = true;
                }
            }
            // fall back on object equality
            else if (!tValue.Equals(newValue))
            {
                tProp.SetValue(target, newValue);
                changed = true;
            }
            
            if(changed)
            {
                this.NotifyPropertyChanged(vmProperty);
                // perform any other actions requested of the setter
                if (secondaryAction != null)
                    secondaryAction();
            }
        }

        /// <summary>
        /// A convenience function for simplifying setters in derived classes.  Accepts references to values directly as a target
        /// </summary>
        /// <param name="target">A reference to the variable that should be modified by the setter</param>
        /// <param name="newValue">The value paramater of the setter</param>
        /// <param name="allowNull">Whether the target can be set to null or not</param>
        /// <param name="secondaryAction">Any action that should be performed after the property value is changed</param>
        /// <param name="targetProperty">The name of the property on the target object.  This must be filled out if the
        /// property on the model is not the same as on the view model</param>
        /// <param name="vmProperty">The name of the property that should be passed to NotifyPropertyChanged.  This must be
        /// filled out if this function is not called from the property itself</param>
        protected void SetProperty<T>(ref T target, T newValue,
            bool allowNull = false, Action secondaryAction = null,
            [CallerMemberName] string targetProperty = "default property",
            [CallerMemberName] string vmProperty = "view model property")
        {
            if (!allowNull && newValue == null)
                return;

            bool changed = false;
            // prefer using the comparable interface
            var tValComp = target as IComparable<T>;
            if(tValComp != null)
            {
                if(tValComp.CompareTo(newValue) != 0)
                {
                    target = newValue;
                    changed = true;
                }
            }
            // fall back on object equality
            else if(!target.Equals(newValue))
            {
                target = newValue;
                changed = true;
            }

            if(changed)
            {
                this.NotifyPropertyChanged(vmProperty);
                // perform any other actions requested of the setter
                if (secondaryAction != null)
                    secondaryAction();
            }
        }

        /// <summary>
        /// Dictionary to contain command bindings created by the GetCommand function
        /// </summary>
        private Dictionary<string, RelayCommand> _commands = new Dictionary<string, RelayCommand>();

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="canExecute">Func that returns true if the command can be executed</param>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Func<bool> canExecute,
            Action execute, [CallerMemberName] string propertyName = "default")
        {
            return this.GetCommand((x => canExecute()), (x => execute()), propertyName);
        }

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="canExecute">Func that returns true if the command can be executed</param>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Func<bool> canExecute,
            Action<object> execute, [CallerMemberName] string propertyName = "default")
        {
            return this.GetCommand((x => canExecute()), execute, propertyName);
        }

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="canExecute">Predicate that returns true if the command can be executed</param>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Predicate<object> canExecute,
            Action execute, [CallerMemberName] string propertyName = "default")
        {
            return this.GetCommand(canExecute, (x => execute()), propertyName);
        }

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="canExecute">Predicate that is used to determine if the command can be executed</param>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Predicate<object> canExecute, 
            Action<object> execute, [CallerMemberName] string propertyName = "default")
        {
            // find the command, and create it if it doesn't exist
            RelayCommand res;
            if(!_commands.TryGetValue(propertyName, out res))
            {
                res = new RelayCommand(execute, canExecute);
                _commands.Add(propertyName, res);
            }

            return res;
        }
    }
}
