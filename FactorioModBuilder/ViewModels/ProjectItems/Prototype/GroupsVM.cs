using FactorioModBuilder.Build;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
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

        public ICommand AddGroupCmd { get { return this.GetCommand(this.AddGroup, this.CanAddGroup);} }
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

        private bool CanAddGroup()
        {
            return true;
        }

        private void AddGroup()
        {
            this.ItemList.Add(new GroupVM(this, 
                new Group("New Group " + _newCount)));
            _newCount++;
        }

        private bool CanRemoveGroup()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveGroup()
        {
            var list = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var g in list)
                this.ItemList.Remove(g);
        }
    }
}
