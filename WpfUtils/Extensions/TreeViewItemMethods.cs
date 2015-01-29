using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfUtils.Extensions
{
    /// <summary>
    /// Extension methods for TreeViewItems
    /// </summary>
    public static class TreeViewItemMethods
    {
        /// <summary>
        /// Determines this TreeViewItem's depth in the visual tree
        /// </summary>
        /// <returns>The number of levels down that this TreeViewItem is in the visual tree</returns>
        public static int GetDepth(this TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }

            return 0;
        }

        /// <summary>
        /// Determines the parent TreeViewItem or TreeView of the given TreeViewItem
        /// </summary>
        /// <param name="item">The TreeViewItem to find the parent for</param>
        /// <returns>The TreeViewItem or TreeView that is the parent of the given TreeViewItem</returns>
        public static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }
    }
}
