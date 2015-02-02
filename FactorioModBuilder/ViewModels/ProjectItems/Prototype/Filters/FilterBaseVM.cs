using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.Menu;
using FactorioModBuilder.ViewModels.Menu.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    /// <summary>
    /// Base clase for project item filters.  Handles synchronization between strongly and weakly typed children collections
    /// </summary>
    /// <typeparam name="TModel">The type of the model to wrap</typeparam>
    /// <typeparam name="TViewModel">The type of the the derived class (CRTP)</typeparam>
    /// <typeparam name="TChildren">The type of the strongly typed children collection</typeparam>
    public abstract class FilterBaseVM<TChildren> : ProjectItem<Filter, FilterBaseVM<TChildren>>, IMenuProvider
        where TChildren : TreeItemVMBase
        
    {
        /// <summary>
        /// Strongly typed children collection that is synchronized with the inherited Children collection
        /// </summary>
        public ObservableCollection<TChildren> TypedChildren { get; private set; }

        /// <summary>
        /// Strongly typed children collection of filters that is synchronized with the inherited Children collection
        /// </summary>
        public ObservableCollection<FilterBaseVM<TChildren>> SubFilters { get; private set; }

        /// <summary>
        /// Provides the list of contenxt menu items for this filter
        /// </summary>
        public ObservableCollection<IMenuItemProvider> MenuItems { get; private set; }

        /// <summary>
        /// The name of the child that will be used in the context menu Add New menu 
        /// </summary>
        public string ChildDisplayName { get; private set; }

        /// <summary>
        /// Base constructor for the filter
        /// </summary>
        /// <param name="name">The display name of the filter</param>
        public FilterBaseVM(string name, string childName = "Item")
            : base(new Filter(name))
        {
            this.ChildDisplayName = childName;
            this.Setup();
        }

        /// <summary>
        /// Base constructor for the Filter
        /// </summary>
        /// <param name="parent">The parent TreeItem node</param>
        /// <param name="name">The display name of the filter</param>
        public FilterBaseVM(TreeItemVMBase parent, string name, string childName = "Item")
            : base(parent, new Filter(name))
        {
            this.ChildDisplayName = childName;
            this.Setup();
        }

        /// <summary>
        /// Method implemented by derived class that will be invoked by the Add New Filter context menu command
        /// </summary>
        /// <returns>A new derived FilterBaseVM</returns>
        protected abstract FilterBaseVM<TChildren> GetNewFilter();

        /// <summary>
        /// Method implemented by derived class that will be invoked by the Add New Item context menu command
        /// </summary>
        /// <returns></returns>
        protected abstract TChildren GetNewChild();

        /// <summary>
        /// Adds a new filter to the subfilter collection
        /// </summary>
        protected void AddFilter()
        {
            this.SubFilters.Add(this.GetNewFilter()); // must call this before attempting to expand
            // must call this after addition
            this.CheckExpandedState();
        }

        /// <summary>
        /// Removes all selected filters from the sub filters collection
        /// </summary>
        protected void RemoveSelectedFilters()
        {
            this.SubFilters.RemoveWhere(o => o.IsSelected);
            // must call this after removal
            this.CheckExpandedState();
        }

        /// <summary>
        /// Adds a new child to the TypedChildren collection
        /// </summary>
        protected void AddChild()
        {
            this.TypedChildren.Add(this.GetNewChild());
            // must call this after addition
            this.CheckExpandedState();
        }

        /// <summary>
        /// Removes all selected children in the TypedChildren
        /// </summary>
        protected void RemoveSelectedChildren()
        {
            this.TypedChildren.RemoveWhere(o => o.IsSelected);
            // must call this after removal
            this.CheckExpandedState();
        }

        /// <summary>
        /// Creates and hooks up the necessary collections and event handlers
        /// and initialized other required properties
        /// </summary>
        private void Setup()
        {
            // collections
            this.TypedChildren = new ObservableCollection<TChildren>();
            this.SubFilters = new ObservableCollection<FilterBaseVM<TChildren>>();
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
            // event handlers
            this.TypedChildren.CollectionChanged += TypedChildrenCollectionChanged;
            this.Children.CollectionChanged += ChildrenCollectionChanged;
            this.SubFilters.CollectionChanged += SubFilterCollectionChanged;
            // menu items
            this.MenuItems.Add(new CategoryItem("Add",
                new ClickableItem("New " + this.ChildDisplayName, this.AddChild),
                new ClickableItem("New " + this.ChildDisplayName + " Filter", this.AddFilter)));
            this.MenuItems.Add(new SeparatorItem());
            this.MenuItems.Add(new ClickableItem("Cut", this.Cut));
            this.MenuItems.Add(new ClickableItem("Copy", this.Copy));
            this.MenuItems.Add(new ClickableItem("Paste", this.Paste, this.CanPaste));
            this.MenuItems.Add(new ClickableItem("Delete", this.Delete));
            this.MenuItems.Add(new ClickableItem("Rename", this.Rename));
            this.MenuItems.Add(new SeparatorItem());
            this.MenuItems.Add(new ClickableItem("Properties", this.ViewProperties));
            // icon
            this.Icon = Resources.Icons.AppIcon.FilterClosed;
        }

        /// <summary>
        /// Sets the icon appropriate depending on whether the filter is expanded or not
        /// </summary>
        protected override void OnIsExpandedChanged()
        {
            if (this.IsExpanded)
                this.Icon = Resources.Icons.AppIcon.FilterOpen;
            else
                this.Icon = Resources.Icons.AppIcon.FilterClosed;
        }

        /// <summary>
        /// Handles synchronization of changes in Children
        /// </summary>
        private void TypedChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TChildren i in e.NewItems)
                    {
                        if (!this.Children.Contains(i))
                            this.Children.Add(i);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (TChildren i in e.OldItems)
                        this.Children.Remove(i);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Children.RemoveWhere(o => o is TChildren);
                    break;
                default:
                    throw new Exception("Unhandled filter collection changed");
            }
        }

        /// <summary>
        /// Handles syncrhonizing changes occuring in Children
        /// </summary>
        private void ChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(TreeItemVMBase i in e.NewItems)
                    {
                        if ((i is TChildren) && !this.TypedChildren.Contains(i))
                            this.TypedChildren.Add((TChildren)i);
                        else if ((i is FilterBaseVM<TChildren>) && !this.SubFilters.Contains(i))
                            this.SubFilters.Add((FilterBaseVM<TChildren>)i);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(TreeItemVMBase i in e.OldItems)
                    {
                        if (i is TChildren)
                            this.TypedChildren.Remove((TChildren)i);
                        else if (i is FilterBaseVM<TChildren>)
                            this.SubFilters.Remove((FilterBaseVM<TChildren>)i);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TypedChildren.Clear();
                    this.SubFilters.Clear();
                    break;
                default:
                    throw new Exception("Unhandled filter collection changed");
            }
        }

        /// <summary>
        /// Handles synchronizing changes occuring in the SubFilters
        /// </summary>
        private void SubFilterCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (FilterBaseVM<TChildren> f in e.NewItems)
                    {
                        if (!this.Children.Contains(f))
                            this.Children.Add(f);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (FilterBaseVM<TChildren> f in e.OldItems)
                        this.Children.Remove(f);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Children.RemoveWhere(o => o is FilterBaseVM<TChildren>);
                    break;
            }
        }

        /// <summary>
        /// Sync visual child state with IsExpanded
        /// </summary>
        private void CheckExpandedState()
        {
            this.IsExpanded = this.Children.Any();
        }

        /// <summary>
        /// Performs the Cut operation on this Filter
        /// </summary>
        protected virtual void Cut()
        {

        }

        /// <summary>
        /// Performs the Copy operation on this Filter
        /// </summary>
        protected virtual void Copy()
        {

        }

        /// <summary>
        /// Performs a Paste operation on this Filter
        /// </summary>
        protected virtual void Paste()
        {

        }

        /// <summary>
        /// Determines if the Paste operation can occur
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanPaste()
        {
            return false;
        }

        /// <summary>
        /// Deletes this filter
        /// </summary>
        protected virtual void Delete()
        {

        }

        /// <summary>
        /// Renames this filter
        /// </summary>
        protected virtual void Rename()
        {

        }

        /// <summary>
        /// Displays the filter properties
        /// </summary>
        protected virtual void ViewProperties()
        {

        }
    }
}
