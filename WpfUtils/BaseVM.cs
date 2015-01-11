using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtils
{
    /// <summary>
    /// The view model base class
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
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
        /// determine appropriate properties to set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The model that contains the data backing the property</param>
        /// <param name="newValue">The value parameter of the setter</param>
        /// <param name="allowNull">Whether the property can be set to null or not</param>
        /// <param name="secondaryAction">Any action that should be performed after the property value is changed</param>
        /// <param name="targetProperty">The name of the property on the target object.  This must be filled out if the
        /// property on the model is not the same as on the view model</param>
        /// <param name="vmProperty">The name of the property that should be passed to NotifyPropertyChanged.  This must be
        /// filled out if this function is not called from the property itself</param>
        protected void PropertySet<T>(object target, T newValue, 
            bool allowNull = false, Action secondaryAction = null,
            [CallerMemberName] string targetProperty = "default property", 
            [CallerMemberName] string vmProperty = "view model property")
        {
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
                    this.NotifyPropertyChanged(vmProperty);
                }
            }
            // fall back on object equality
            else if (!tValue.Equals(newValue))
            {
                tProp.SetValue(target, newValue);
                this.NotifyPropertyChanged(vmProperty);
            }
            else
                return; // do nothing since we did not have to change the value
            
            // perform any other actions requested of the setter
            if (secondaryAction != null)
                secondaryAction();
        }
    }
}
