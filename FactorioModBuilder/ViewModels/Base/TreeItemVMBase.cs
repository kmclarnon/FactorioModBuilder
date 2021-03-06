﻿using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using System.ComponentModel;
using FactorioModBuilder.Resources.Icons;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows.Data;

namespace FactorioModBuilder.ViewModels.Base
{
    /// <summary>
    /// The base view model that wraps a TreeItem and exposes the appropriate methods to handle
    /// selection, expand/collapse operations and display of children
    /// </summary>
    public abstract class TreeItemVMBase : BaseVM, IDataErrorInfo, IDisplayState
    {
        /// <summary>
        /// An event handler for the InitCompleted event fired after the construction and initialization
        /// of the entire tree view
        /// </summary>
        public delegate void InitCompleteHandler(TreeItemVMBase sender, EventArgs e);

        /// <summary>
        /// An event fired after the constructor for the top level tree item view model has finished
        /// </summary>
        public event InitCompleteHandler OnInitComplete;

        /// <summary>
        /// The immediant parent of this tree item view model
        /// </summary>
        public TreeItemVMBase Parent { get; private set; }

        /// <summary>
        /// View for the children collection
        /// </summary>
        public ICollectionView ChildrenView { get; private set; }

        /// <summary>
        /// The children owned by this tree item view model.  Children are displayed below and indented in the TreeView
        /// when the property IsExpanded is true.
        /// </summary>
        public ObservableCollection<TreeItemVMBase> Children { get; private set; }

        /// <summary>
        /// The expanded state of this tree item view model. True indicates that this item is currently expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value, false, null, (x => this.OnIsExpandedChanged()), (x => x ? this.Children.Any() : true)); }
        }

        /// <summary>
        /// The selection state of this tree item view model. True indicates that this item is currently selected
        /// </summary>
        public bool IsSelected
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value, false, null, (x => this.OnIsSelectedChangedInternal())); }
        }

        /// <summary>
        /// Used for selection binding when this node is referenced outside of the treeview
        /// </summary>
        public bool ContentIsSelected
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The name of this tree item view model
        /// </summary>
        public string Name
        {
            get { return this.GetProperty<string>(null, null, this.OnGetName); }
            set { this.SetProperty(value, false, null, (x => this.OnNameChanged())); }
        }

        /// <summary>
        /// The name value used to determine sort order
        /// </summary>
        public string SortName
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => { if(this.ChildrenView != null) this.ChildrenView.Refresh(); })); }
        }

        /// <summary>
        /// The current error message exposed 
        /// </summary>
        public string Error
        {
            get { throw new NotImplementedException("Classes derived from TreeItemVMBase must implemented the Error property"); }
        }

        /// <summary>
        /// The Icon that should be used to represent this TreeItem
        /// </summary>
        public AppIcon Icon
        {
            get { return this.GetProperty<AppIcon>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// How this item should be displayed by the treview
        /// </summary>
        public DisplayState DisplayState
        {
            get { return this.GetProperty<DisplayState>(); }
            private set { this.SetProperty(value, false, null, (x => this.OnDisplayStateChanged())); }
        }

        /// <summary>
        /// The selected nodes in this tree
        /// </summary>
        public IEnumerable<TreeItemVMBase> SelectedNodes
        {
            get
            {
                List<TreeItemVMBase> res = new List<TreeItemVMBase>();
                this.GetSelected(res);
                return res;
            }
        }

        /// <summary>
        /// Indexer used to access IDataErrorInfo messages
        /// </summary>
        /// <returns>The associated error created as a result of a property data error</returns>
        public abstract string this[string name] { get; }

        /// <summary>
        /// The TreeItem contained by this TreeItemVM
        /// </summary>
        protected TreeItemBase _item;

        /// <summary>
        /// A gaurd variable to prevent multiple initialization-
        /// </summary>
        private bool _initOnce = false;

        /// <summary>
        /// The base constructor used to wrap a TreeItem in a view model
        /// </summary>
        /// <param name="item">The tree item that will back the view model</param>
        public TreeItemVMBase(TreeItemBase item)
            : this(null, item)
        {
        }

        /// <summary>
        /// The constructor used to wrap a TreeItem in a view model with a specified parent
        /// </summary>
        /// <param name="parent">The parent TreeItemVM of the view model</param>
        /// <param name="item">The TreeItem that will back the view model</param>
        public TreeItemVMBase(TreeItemVMBase parent, TreeItemBase item)
            : this(parent, item, new List<TreeItemVMBase>())
        {
        }

        /// <summary>
        /// The constructor used to wrap a TreeItem in a view model with children view models
        /// </summary>
        /// <param name="item"></param>
        /// <param name="children"></param>
        public TreeItemVMBase(TreeItemBase item, IEnumerable<TreeItemVMBase> children)
            : this(null, item, children)
        {
        }

        /// <summary>
        /// The constructor used to wrap a TreeItem in a view model with the specified view model
        /// parent and children
        /// </summary>
        /// <param name="parent">The parent TreeItem view model</param>
        /// <param name="item">The TreeItem that will back this view model</param>
        /// <param name="children">The TreeItem view models to be displayed as children of this view model</param>
        public TreeItemVMBase(TreeItemVMBase parent, TreeItemBase item, IEnumerable<TreeItemVMBase> children)
            : base(item)
        {
            Parent = parent;
            _item = item;
            this.Children = new ObservableCollection<TreeItemVMBase>();
            this.ChildrenView = CollectionViewSource.GetDefaultView(this.Children);
            this.Children.CollectionChanged += OnChildrenCollectionChanged;
            foreach (var c in children)
                this.Children.Add(c);
            this.SetupChildrenView();
            if(Parent != null)
                parent.OnInitComplete += ParentInitComplete;
        }

        /// <summary>
        /// Handles collection changed events on the children's collection
        /// </summary>
        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(TreeItemVMBase i in e.NewItems)
                    {
                        i.Parent = this;
                        this.OnInitComplete += i.ParentInitComplete;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(TreeItemVMBase i in e.OldItems)
                    {
                        if (ReferenceEquals(this, i.Parent))
                            i.Parent = null;
                        this.OnInitComplete -= i.ParentInitComplete;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach(TreeItemVMBase oi in e.OldItems)
                    {
                        if (ReferenceEquals(this, oi.Parent))
                            oi.Parent = null;
                        this.OnInitComplete -= oi.ParentInitComplete;
                    }
                    foreach(TreeItemVMBase ni in e.NewItems)
                    {
                        ni.Parent = this;
                        this.OnInitComplete += ni.ParentInitComplete;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach(TreeItemVMBase i in e.OldItems)
                    {
                        if (ReferenceEquals(this, i.Parent))
                            i.Parent = null;
                        this.OnInitComplete -= i.ParentInitComplete;
                    }
                    break;
                default:
                    throw new Exception("Unhandled Children Collection Changed");
            }
        }

        /// <summary>
        /// Called when the parent signals that its initialization is complete, this method
        /// performs any initialization actions required and then signals initialization is complete to
        /// any children contained in this view model
        /// </summary>
        private void ParentInitComplete(TreeItemVMBase sender, EventArgs e)
        {
            this.Initialize();
            this.InitComplete();
        }

        /// <summary>
        /// Contains any initialization functionality that must be performed
        /// after the parent has been fully constructed and initialized.  Derived classes
        /// do not need to call base.Initialize()
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Sets up the filtering, sorting, etc of the ChildrenView. By default children are sorted ascending by name
        /// </summary>
        protected virtual void SetupChildrenView()
        {
            this.ChildrenView.SortDescriptions.Add(
                new SortDescription("SortName", ListSortDirection.Ascending));
        }

        /// <summary>
        /// Signals that this object has been fully constructed and initialized
        /// </summary>
        protected void InitComplete()
        {
            if(!_initOnce && this.OnInitComplete != null)
            {
                this.OnInitComplete(this, new EventArgs());
                _initOnce = true;
            }
        }

        /// <summary>
        /// Called when the name property is retrieved.  May modify the value returned from that property
        /// </summary>
        /// <param name="name">The current value of the name property</param>
        /// <returns>The modified value of the name property</returns>
        protected virtual string OnGetName(string name)
        {
            return name;
        }

        /// <summary>
        /// Called when the Name property is changed
        /// </summary>
        protected virtual void OnNameChanged()
        {
        }

        /// <summary>
        /// Returns the item state to normal and calls OnIsSelectedChanged
        /// </summary>
        private void OnIsSelectedChangedInternal()
        {
            this.DisplayState = Base.DisplayState.Normal;
            this.OnIsSelectedChanged();
        }

        /// <summary>
        /// Called when the IsSelected property is changes
        /// </summary>
        protected virtual void OnIsSelectedChanged()
        {
        }

        /// <summary>
        /// Called when the IsExpanded property is changes
        /// </summary>
        protected virtual void OnIsExpandedChanged()
        {
            if (this.IsExpanded && Parent != null) 
                Parent.IsExpanded = true; 
        }

        /// <summary>
        /// Called when the DisplayState property changes
        /// </summary>
        protected virtual void OnDisplayStateChanged()
        {
        }

        /// <summary>
        /// Puts this node into the renaming state
        /// </summary>
        public virtual void DoRename()
        {
            this.DisplayState = Base.DisplayState.Renaming;
        }

        /// <summary>
        /// Recursively expands this and all contained children
        /// </summary>
        public void ExpandDown()
        {
            this.IsExpanded = true;
            foreach (var c in this.Children)
                c.ExpandDown();
        }

        /// <summary>
        /// Recursively expands this an all contained children the specified number of levels down
        /// </summary>
        /// <param name="levels"></param>
        public void ExpandDown(int levels)
        {
            if (levels < 1)
                return;
            this.IsExpanded = true;
            foreach (var c in this.Children)
                c.ExpandDown(levels - 1);
        }

        /// <summary>
        /// Attempts to find a parent of the specified type contained in the view model tree
        /// </summary>
        /// <typeparam name="T">The type of the parent to search for</typeparam>
        /// <param name="element">The value to be set to the parent if the search is successful</param>
        /// <returns>True if the appropriate parent is found, false otherwise</returns>
        public bool TryFindElementUp<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            var res = (TreeItemVMBase)Parent;
            while (res != null)
            {
                if (res.GetType() == typeof(T))
                {
                    element = (T)res;
                    return true;
                }
                else
                    res = (TreeItemVMBase)res.Parent;
            }

            return false;
        }

        /// <summary>
        /// Attempts to find a child of the specified type contained in the view model tree
        /// </summary>
        /// <typeparam name="T">The type of the parent to search for</typeparam>
        /// <param name="element">The value to be set to the child if the search is successful</param>
        /// <returns>True if the appropriate child is found, false otherwise</returns>
        public bool TryFindElementDown<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            foreach (var c in this.Children)
            {
                if (c.GetType() == typeof(T))
                {
                    element = (T)c;
                    return true;
                }

                if (c.TryFindElementDown<T>(out element))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to find a peer of the specified type contained in the view model tree.  Peers
        /// are defined as children of this item's parent.  Excludes the calling object from the search
        /// </summary>
        /// <typeparam name="T">The type of the peer to search for</typeparam>
        /// <param name="element">The value to be set to the peer if the search is successfull</param>
        /// <returns>True if the appropriate peer is found, false otherwise</returns>
        public bool TryFindElementPeer<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            TreeItemVMBase res = (TreeItemVMBase)Parent;
            if (res == null)
                return false;
            else
            {
                foreach(var c in res.Children)
                {
                    if(c.GetType() == typeof(T) && !ReferenceEquals(c, this))
                    {
                        element = (T)c;
                        return true;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Populates the given list with all selected nodes in this tree
        /// </summary>
        /// <param name="result">The selected item collection</param>
        private void GetSelected(List<TreeItemVMBase> result)
        {
            if (this.IsSelected)
                result.Add(this);
            foreach (var c in this.Children)
                c.GetSelected(result);
        }
    }
}
