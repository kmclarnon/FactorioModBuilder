using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfUtils.Behaviors.Controls
{
    /// <summary>
    /// Behavior that allows a treeview to select the node right-clicked on before the right click event
    /// </summary>
    public class TreeViewRightClickSelect
    {
        /// <summary>
        /// The right click select dependency property
        /// </summary>
        public static readonly DependencyProperty ActiveProperty =
            DependencyProperty.RegisterAttached("Active",
                typeof(bool),
                typeof(TreeViewRightClickSelect),
                new UIPropertyMetadata(ActiveChanged));

        /// <summary>
        /// Sets the right click select property value
        /// </summary>
        public static void SetActive(DependencyObject target, bool value)
        {
            target.SetValue(ActiveProperty, value);
        }

        /// <summary>
        /// Handles hooking and unhooking the events required to handle right click selection
        /// </summary>
        public static void ActiveChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            TreeView tree = target as TreeView;
            if (tree != null)
            {
                if ((bool)e.NewValue && !(bool)e.OldValue)
                    tree.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
                else if (!(bool)e.NewValue && (bool)e.OldValue)
                    tree.PreviewMouseRightButtonDown -= OnPreviewMouseRightButtonDown;
            }
        }

        /// <summary>
        /// Handles focusing the item selected before the right click
        /// </summary>
        private static void OnPreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Finds the TreeViewItem owner of the source object
        /// </summary>
        private static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
    }
}
