using FactorioModBuilder.Build;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Build.Data;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class GroupsVM : ProjectItem<Groups, GroupsVM>
    {
        public ObservableCollection<GroupVM> ItemList { get; private set; }

        public override IEnumerable<DataUnit> CompilerData
        {
            get 
            {
                return this.ItemList.SelectMany(o => o.CompilerData);
            }
        }

        public ICommand AddGroupCmd { get { return this.GetCommand(this.AddGroup);} }
        public ICommand RemoveGroupCmd { get { return this.GetCommand(this.RemoveGroup, this.CanRemoveGroup); } }

        private int _newCount = 1;

        public GroupsVM(Groups groups)
            : this(null, groups)
        {
        }

        public GroupsVM(TreeItemVMBase parent, Groups groups)
            : base(parent, groups)
        {
            this.ItemList = new ObservableCollection<GroupVM>();
        }

        /// <summary>
        /// Adds a new group to the ItemList collection
        /// </summary>
        private void AddGroup()
        {
            this.ItemList.Add(new GroupVM(this, 
                new Group("new-group-" + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Determines if any groups can be removed from the ItemList collection
        /// </summary>
        /// <returns>True if any groups are selected, otherwise false</returns>
        private bool CanRemoveGroup()
        {
            return this.ItemList.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected groups from the ItemList collection
        /// </summary>
        private void RemoveGroup()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
