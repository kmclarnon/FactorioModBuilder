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
            this.SubGroups = new ObservableCollection<SubGroupVM>();
            this.RecusiveTypedChildren.CollectionChanged += OnGroupsCollectionChanged;
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
