using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.Menu;
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
    /// A filter for Group prototypes
    /// </summary>
    public class GroupsFilterVM : FilterBaseVM<GroupVM>
    {
        /// <summary>
        /// Groups contained by this group filter and its children
        /// </summary>
        public ObservableCollection<GroupVM> Groups { get; private set; }

        /// <summary>
        /// Subgroups contained in the this filters groups collection
        /// </summary>
        public ObservableCollection<SubGroupVM> SubGroups { get; private set; }

        /// <summary>
        /// Counter to keep track of new groups created
        /// </summary>
        private int _newChild = 1;

        public GroupsFilterVM(string name)
            : base(name, "Group")
        {
            this.Groups = new ObservableCollection<GroupVM>();
            this.SubGroups = new ObservableCollection<SubGroupVM>();
            this.TypedChildren.CollectionChanged += OnTypedChildrenCollectionChanged;
            this.SubFilters.CollectionChanged += OnSubFiltersCollectionChanged;
            this.Groups.CollectionChanged += OnGroupsCollectionChanged;
        }

        /// <summary>
        /// Handles synchronizing available subgroups from all groups
        /// </summary>
        void OnGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(GroupVM ni in e.NewItems)
                    {
                        ni.Children.CollectionChanged += OnGroupChildrenCollectionChanged;
                        foreach (SubGroupVM sg in ni.Children)
                            this.SubGroups.Add(sg);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (GroupVM ni in e.OldItems)
                    {
                        ni.Children.CollectionChanged -= OnGroupChildrenCollectionChanged;
                        foreach (SubGroupVM sg in ni.Children)
                            this.SubGroups.Remove(sg);
                    }
                    break;
                default:
                    throw new Exception("Unhandled Typed Children CollectionChanged event action");
            }
        }

        /// <summary>
        /// Handles synchronization between subgroups in each group and the subgroups collection
        /// </summary>
        private void OnGroupChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (SubGroupVM ni in e.NewItems)
                        this.SubGroups.Add(ni);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (SubGroupVM oi in e.OldItems)
                        this.SubGroups.Remove(oi);
                    break;
                default:
                    throw new Exception("Unhandled Typed Children CollectionChanged event action");
            }
        }

        /// <summary>
        /// Handles synchronizing the direct changes in GroupVMs held by this filter
        /// </summary>
        private void OnTypedChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (GroupVM g in e.NewItems)
                        this.Groups.Add(g);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (GroupVM g in e.OldItems)
                        this.Groups.Remove(g);
                    break;
                default:
                    throw new Exception("Unhandled Typed Children CollectionChanged event action");
            }
        }

        /// <summary>
        /// Handles synchronizing the indirect changes in GroupVMs held by this filter
        /// </summary>
        private void OnSubFiltersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach(GroupsFilterVM ni in e.NewItems)
                    {
                        ni.Groups.CollectionChanged += OnSubFiltersGroupsCollectionChanged;
                        foreach (var g in ni.Groups)
                            this.Groups.Add(g);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(GroupsFilterVM oi in e.OldItems)
                    {
                        oi.Groups.CollectionChanged -= OnSubFiltersGroupsCollectionChanged;
                        foreach (var g in oi.Groups)
                            this.Groups.Remove(g);
                    }
                    break;
                default:
                    throw new Exception("Unhandled Typed Children CollectionChanged event action");
            }
        }

        /// <summary>
        /// Handles syncronizing subfilters groups collection
        /// </summary>
        private void OnSubFiltersGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (GroupVM ni in e.NewItems)
                        this.Groups.Add(ni);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (GroupVM oi in e.OldItems)
                        this.Groups.Remove(oi);
                    break;
                default:
                    throw new Exception("Unhandled Typed Children CollectionChanged event action");
            }
        }

        protected override FilterBaseVM<GroupVM> GetNewFilter()
        {
            return new GroupsFilterVM("New Filter");
        }

        protected override GroupVM GetNewChild()
        {
            return new GroupVM(new Group("New Group " + _newChild++));
        }
    }
}
