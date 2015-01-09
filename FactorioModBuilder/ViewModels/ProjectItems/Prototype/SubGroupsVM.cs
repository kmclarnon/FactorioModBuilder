﻿using FactorioModBuilder.Build;
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
    public class SubGroupsVM : ProjectItem<SubGroups>
    {
        public ObservableCollection<SubGroupVM> ItemList { get; private set; }

        public override Build.CompileUnit CompilerData
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
            get { return "prototypes.subgroups"; }
        }

        public ObservableCollection<GroupVM> PossibleGroups
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp<PrototypesVM>(out res))
                    throw new Exception("Failed to find parent to supply PossibleGroups");
                return res.PossibleGroups;
            }
        }

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

        private int _newCount = 1;

        public SubGroupsVM(SubGroups items)
            : this(null, items)
        {
        }

        public SubGroupsVM(TreeItemVMBase parent, SubGroups items)
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
            this.ItemList.Add(new SubGroupVM(this, 
                new SubGroup("New Subgroup " + _newCount)));
            _newCount++;
        }

        private bool CanRemoveSubgroup()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveSubgroup()
        {
            var list = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var i in list)
                this.ItemList.Remove(i);
        }
    }
}
