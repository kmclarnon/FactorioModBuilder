using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfUtils.Behaviors.Controls
{
    public class TextBoxSelectTextOnFocus
    {
        /// <summary>
        /// The right click select dependency property
        /// </summary>
        public static readonly DependencyProperty RightClickSelectProperty =
            DependencyProperty.RegisterAttached("Active",
                typeof(bool),
                typeof(TextBoxSelectTextOnFocus),
                new UIPropertyMetadata(ActiveChanged));

        /// <summary>
        /// Sets the right click select property value
        /// </summary>
        public static void SetActive(DependencyObject target, bool value)
        {
            target.SetValue(RightClickSelectProperty, value);
        }

        /// <summary>
        /// Handles hooking and unhooking the events required to handle right click selection
        /// </summary>
        public static void ActiveChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = target as TextBox;
            if(tb != null)
            {
                if ((bool)e.NewValue && !(bool)e.OldValue)
                    tb.GotFocus += OnGotFocus;
                else if (!(bool)e.NewValue && (bool)e.OldValue)
                    tb.GotFocus -= OnGotFocus;
            }
        }

        /// <summary>
        /// Highlights the text in the textbox when the control recieves focus
        /// </summary>
        static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb != null)
                tb.SelectAll();
        }

    }
}
