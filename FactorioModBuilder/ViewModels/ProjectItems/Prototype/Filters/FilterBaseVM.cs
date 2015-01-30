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
    public class FilterBaseVM<TChildren> : ProjectItem<Filter, FilterBaseVM<TChildren>>, IMenuProvider
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
        /// Counter to keep track of new filters added
        /// </summary>
        private int _newFilterCount = 1;

        /// <summary>
        /// Base constructor for the filter
        /// </summary>
        /// <param name="name">The display name of the filter</param>
        public FilterBaseVM(string name)
            : base(new Filter(name))
        {
            this.SetupCollections();
        }

        /// <summary>
        /// Base constructor for the Filter
        /// </summary>
        /// <param name="parent">The parent TreeItem node</param>
        /// <param name="name">The display name of the filter</param>
        public FilterBaseVM(TreeItemVMBase parent, string name)
            : base(parent, new Filter(name))
        {
            this.SetupCollections();
        }

        /// <summary>
        /// Adds a new filter to the subfilter collection
        /// </summary>
        protected void AddFilter()
        {
            this.SubFilters.Add(new FilterBaseVM<TChildren>("New " + this.Name + " Filter " + _newFilterCount));
            _newFilterCount++;
        }

        /// <summary>
        /// Removes all selected filters from the sub filters collection
        /// </summary>
        protected void RemoveSelectedFilters()
        {
            this.SubFilters.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Adds a new child to the TypedChildren collection
        /// </summary>
        protected void AddChild(TChildren child)
        {
            this.TypedChildren.Add(child);
        }

        /// <summary>
        /// Removes all selected children in the TypedChildren
        /// </summary>
        protected void RemoveSelectedChildren()
        {
            this.TypedChildren.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Creates and hooks up the necessary collections and event handlers
        /// </summary>
        private void SetupCollections()
        {
            // collections
            this.TypedChildren = new ObservableCollection<TChildren>();
            this.SubFilters = new ObservableCollection<FilterBaseVM<TChildren>>();
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
            // event handlers
            this.TypedChildren.CollectionChanged += ChildrenCollectionChanged;
            this.Children.CollectionChanged += ChildrenCollectionChanged;
            this.SubFilters.CollectionChanged += ChildrenCollectionChanged;
        }

        /// <summary>
        /// Handles syncrhonization between TypedChildren and Children
        /// </summary>
        private void ChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(sender == this.TypedChildren)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach(TChildren i in e.NewItems)
                        {
                            if (!this.Children.Contains(i))
                                this.Children.Add(i);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach(TChildren i in e.OldItems)
                            this.Children.Remove(i);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        
                        break;
                    default:
                        throw new Exception("Unhandled filter collection changed");
                }
            }
            else if(sender == this.Children)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach(TreeItemVMBase i in e.NewItems)
                        {
                            if ((i is TChildren) && !this.TypedChildren.Contains(i))
                                this.TypedChildren.Add((TChildren)i);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach(TreeItemVMBase i in e.OldItems)
                        {
                            if (i is TChildren)
                                this.TypedChildren.Remove((TChildren)i);
                        }
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        this.TypedChildren.Clear();
                        break;
                    default:
                        throw new Exception("Unhandled filter collection changed");
                }
            }
            else if(sender == this.SubFilters)
            {
                switch(e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach(FilterBaseVM<TChildren> f in e.NewItems)
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
                        break;
                }
            }
        }
    }
}
