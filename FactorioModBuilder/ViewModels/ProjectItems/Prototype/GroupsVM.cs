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

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class GroupsVM : ProjectItem<Groups>
    {
        public ObservableCollection<GroupVM> ItemList { get; private set; }

        public override CompileUnit CompilerData
        {
            get 
            { 
                return new CompileUnit(this.ItemList
                    .Where(o => o.Enabled)
                    .Select(o => o.CompilerData)); 
            }
        }

        public override string CompilerKey
        {
            get { return "prototypes.groups"; }
        }

        private ICommand _addGroupCmd;
        public ICommand AddGroupCmd
        {
            get
            {
                if (_addGroupCmd == null)
                    _addGroupCmd = new RelayCommand(
                        (x => this.AddGroup()), (x => this.CanAddGroup()));
                return _addGroupCmd;
            }
        }

        private ICommand _removeGroupCmd;
        public ICommand RemoveGroupCmd
        {
            get
            {
                if (_removeGroupCmd == null)
                    _removeGroupCmd = new RelayCommand(
                        (x => this.RemoveGroup()), (x => this.CanRemoveGroup()));
                return _removeGroupCmd;
            }
        }

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
