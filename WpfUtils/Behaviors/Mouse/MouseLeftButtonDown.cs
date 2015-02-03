using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUtils.Behaviors.Mouse
{
    /// <summary>
    /// Behavior that allows binding an ICommand to the MosueLeftButtonDown event of a Control
    /// </summary>
    public class MouseLeftButtonDown
    {
        /// <summary>
        /// ICommand binding property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(MouseLeftButtonDown),
            new UIPropertyMetadata(CommandChanged));

        /// <summary>
        /// ICommand paramaeter binding property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
            typeof(object),
            typeof(MouseLeftButtonDown),
            new UIPropertyMetadata(null));

        /// <summary>
        /// Handles setting the command property
        /// </summary>
        public static void SetCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Handles setting the command parameter property
        /// </summary>
        public static void SetCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Handles getting the command parameter property
        /// </summary>
        public static object GetCommandParameter(DependencyObject target)
        {
            return target.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Handles hooking and unhooking the MouseLeftButtonDown event
        /// </summary>
        private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Control control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.MouseLeftButtonDown += OnMouseLeftButtonDown;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }
        }

        /// <summary>
        /// Handles invoking the bounding command
        /// </summary>
        private static void OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
            {
                // get the command and the associated parameter and invoke the command
                ICommand command = (ICommand)control.GetValue(CommandProperty);
                object commandParameter = control.GetValue(CommandParameterProperty);
                command.Execute(commandParameter);
            }
        }
    }
}
