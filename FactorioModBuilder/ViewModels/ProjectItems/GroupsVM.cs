using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GroupsVM : ProjectItemVM
    {
        public ObservableCollection<GroupVM> ItemList { get; private set; }

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

        public GroupsVM(ProjectItemVM parent, Groups groups)
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
            
        }

        private bool CanRemoveGroup()
        {
            return true;
        }

        private void RemoveGroup()
        {

        }
    }
}
