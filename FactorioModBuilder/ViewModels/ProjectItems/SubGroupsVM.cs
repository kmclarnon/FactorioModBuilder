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
    public class SubGroupsVM : ProjectItemVM
    {
        public ObservableCollection<SubGroupVM> ItemList { get; private set; }

        private ICommand _addSubgroupCmd;
        public ICommand AddSubgroupCmd
        {
            get
            {
                if (_addSubgroupCmd == null)
                    _addSubgroupCmd = new RelayCommand(
                        (x => this.AddSubgroup()), (x => this.CanAddSubgroup()));
                return _addSubgroupCmd;
            }
        }

        private ICommand _removeSubgroupCmd;
        public ICommand RemoveSubgroupCmd
        {
            get
            {
                if (_removeSubgroupCmd == null)
                    _removeSubgroupCmd = new RelayCommand(
                        (x => this.RemoveSubgroup()), (x => this.CanRemoveSubgroup()));
                return _removeSubgroupCmd;
            }
        }

        public SubGroupsVM(ProjectItemVM parent, SubGroups items)
            : base(parent, items)
        {
            this.ItemList = new ObservableCollection<SubGroupVM>();
        }

        private bool CanAddSubgroup()
        {
            return true;
        }

        private void AddSubgroup()
        {

        }

        private bool CanRemoveSubgroup()
        {
            return true;
        }

        private void RemoveSubgroup()
        {

        }
    }
}
