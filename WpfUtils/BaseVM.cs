﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils.Extensions;

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
        /// Dictionary to contain command bindings created by the GetCommand function
        /// </summary>
        private Dictionary<string, RelayCommand> _commands = new Dictionary<string, RelayCommand>();

        /// <summary>
        /// Dictionary to contain properties generated with SetProperty&lt;T&gt;(T value).  Can be retrieved through GetProperty&lt;T&gt;()
        /// </summary>
        protected Dictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Provides a convenient way to send notifications.  When called inside a property,
        /// it can be called safely with the default paramater
        /// </summary>
        /// <param name="property">The name of the property that is changing. This defaults to [CallerMemberName]</param>
        protected void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Provides a convenient way to send notifications outside of a property by taking an
        /// member expression which then supports refactoring
        /// </summary>
        /// <param name="selector">The member expression that selects the property</param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> selector)
        {
            this.NotifyPropertyChanged(PropertyMethods.GetName(selector));
        }
    
        /// <summary>
        /// A convenience function for simplifying setters in derived classes.  Uses reflection to 
        /// determine appropriate properties to set and attempts to compare using IComparable&lt;T&gt;
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
            [CallerMemberName] string targetProperty = "", 
            [CallerMemberName] string vmProperty = "")
        {
            if (target == null)
                throw new ArgumentNullException("Target object cannot be null");

            // we must check for null before comparing types because
            // a null value will fail the type comparison
            if (!allowNull && newValue == null)
                return; // not allowed

            bool changed = false;
            var tType = target.GetType();
            // get our target property's value to work with
            var tProp = tType.GetProperty(targetProperty);
            if (newValue != null && tProp.PropertyType != typeof(T))
                throw new ArgumentException("Referenced property is not of type: " + typeof(T).FullName);

            var tValue = tProp.GetValue(target);
            if(tValue != null)
            {
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
            }
            else
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
            [CallerMemberName] string targetProperty = "",
            [CallerMemberName] string vmProperty = "")
        {
            // we must check for null before comparing types because
            // a null value will fail the type comparison
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
        /// Sets the property indicated by the propertyName argument to the given value.  
        /// Creates a new property and assigns it the given value if it does not exist yet
        /// </summary>
        /// <param name="value">The new property value</param>
        /// <param name="secondaryAction">An action that should be performed after a the value has changed</param>        
        /// <param name="allowNull">Whether the property can be set to null or not</param>
        /// <param name="propertyName">The name of the property.  Defaults to CallerMemberName and only needs to be set when invoked outside a property setter</param>
        protected void SetProperty<T>(T value,
            Action<T> precedingAction = null,
            Action secondaryAction = null,
            bool allowNull = false,
            [CallerMemberName] string propertyName = "")
        {
            // we must check for null before comparing types because
            // a null value will fail the type comparison
            if (!allowNull && value == null)
                return; // not allowed

            // check for type consistency.  We use reflection here because the
            // initial value of the property can be null
            var pInfo = this.GetType().GetProperty(propertyName);
            if (!pInfo.PropertyType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Supplied type does not match property type");

            // execute our preceeding action if we have one
            if (precedingAction != null)
                precedingAction(value);

            bool changed = false;
            // ensure type consistency
            object res;
            if (_properties.TryGetValue(propertyName, out res))
            {
                if(res == null)
                {
                    _properties[propertyName] = value;
                    changed = true;
                }
                else
                {
                    // prefer IComparable
                    var compRes = res as IComparable<T>;
                    if (compRes != null)
                    {
                        if (compRes.CompareTo(value) != 0)
                        {
                            _properties[propertyName] = value;
                            changed = true;
                        }
                    }
                    // fall back on objet equality
                    else if (!res.Equals(value))
                    {
                        _properties[propertyName] = value;
                        changed = true;
                    }
                }
            }
            // create the property if it doesn't already exist
            else
            {
                _properties[propertyName] = value;
                changed = true;
            }

            if(changed)
            {
                this.NotifyPropertyChanged(propertyName);
                // perform any other actions requested of the setter
                if (secondaryAction != null)
                    secondaryAction();
            }

        }

        /// <summary>
        /// Gets the property indiciated by the propertyName argument.
        /// Creates a new property entry and returns the default value if the property does not exist yet
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve the value for.  Defaults to CallerMemberName and only needs to be set when invoked outside of the property get</param>
        /// <returns>The value associated with the property or default(T) if the property does not exist yet</returns>
        protected T GetProperty<T>([CallerMemberName] string propertyName = "")
        {
            object res;
            if (!_properties.TryGetValue(propertyName, out res))
            {
                _properties.Add(propertyName, default(T));
                return default(T);
            }

            var pInfo = this.GetType().GetProperty(propertyName);
            if (!pInfo.PropertyType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Property type does not match expected type");
            return (T)res;
        }

        /// <summary>
        /// Gets the property indiciated by the propertyName argument on the source object
        /// </summary>
        /// <typeparam name="T">The type of the property to retrieve</typeparam>
        /// <param name="source">The item that contains the property to access</param>
        /// <param name="propertyName">The name of the property to access on the provided item</param>
        /// <returns>The value of the specified property on the source object</returns>
        protected T GetProperty<T>(object source, [CallerMemberName] string propertyName = "")
        {
            return this.GetProperty<T>(source, (() => { }), propertyName);
        }

        /// <summary>
        /// Gets the property indiciated by the propertyName argument on the source object and performs any secondary actions
        /// </summary>
        /// <typeparam name="T">The type of the property to retrieve</typeparam>
        /// <param name="source">The item that contains the property to access</param>
        /// <param name="precedingAction">Action to be executed before the property is retrieved</param>
        /// <param name="propertyName">The name of the property to access on the provided item</param>
        /// <returns>The value of the specified property on the source object</returns>
        protected T GetProperty<T>(object source, Action precedingAction, [CallerMemberName] string propertyName = "")
        {
            var pInfo = source.GetType().GetProperty(propertyName);
            if (pInfo == null || !pInfo.PropertyType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Property does not match expected type");

            // execute our action if we have one
            if (precedingAction != null)
                precedingAction();

            return (T)pInfo.GetValue(source);
        }

        /// <summary>
        /// Gets the modified property indiciated by the propertyName argument on the source object
        /// </summary>
        /// <typeparam name="T">The type of the property to retrieve</typeparam>
        /// <param name="source">The item that contains the property to access</param>
        /// <param name="modifier">The function used to modify the value returned for this property</param>
        /// <param name="propertyName">The name of the property to access on nthe provided item</param>
        /// <returns>The result of modifier, given the value of the property on the source object</returns>
        protected T GetProperty<T>(object source, Func<T,T> modifier, [CallerMemberName] string propertyName = "")
        {
            if (source == null)
                throw new ArgumentException("source");
            var pInfo = source.GetType().GetProperty(propertyName);
            if (pInfo == null || !pInfo.PropertyType.IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Property does not match expected type");

            // execute our modifier if we have one
            if (modifier != null)
                return modifier((T)pInfo.GetValue(source));
            else
                return (T)pInfo.GetValue(source);
        }

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="canExecute">Func that returns true if the command can be executed</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Action execute, Func<bool> canExecute = null,
            [CallerMemberName] string propertyName = "default")
        {
            return this.GetCommand((x => execute()), (x => (canExecute == null) ? true : canExecute()), propertyName);
        }

        /// <summary>
        /// Provides a convenient way to implement command binding in the view model
        /// </summary>
        /// <param name="execute">Action to be performed by the command</param>
        /// <param name="canExecute">Predicate that is used to determine if the command can be executed</param>
        /// <param name="propertyName">Property name to associate the command with.  Defaults to CallerMemberName</param>
        /// <returns>The command composed of the supplied functions</returns>
        protected ICommand GetCommand(Action<object> execute, Predicate<object> canExecute = null, 
            [CallerMemberName] string propertyName = "default")
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
