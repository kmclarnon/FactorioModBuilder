using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupVM : TreeItemVM<SubGroup>
    {
        public string Type
        {
            get { return _internal.Type; }
            set
            {
                if(_internal.Type != value)
                {
                    _internal.Type = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private GroupVM _selectedGroup;
        public GroupVM SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if(_selectedGroup != value)
                {
                    _selectedGroup = value;
                    this.NotifyPropertyChanged();
                    if (value != null)
                        this.Group = _selectedGroup.Name;
                }
            }
        }

        public string Group
        {
            get { return _internal.Group; }
            set
            {
                if(_internal.Group != value)
                {
                    _internal.Group = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Order
        {
            get { return _internal.Order; }
            set
            {
                if(_internal.Order != value)
                {
                    _internal.Order = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if(_enabled != value)
                {
                    _enabled = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public SubGroupVM(SubGroup item)
            : this(null, item)
        {
        }

        public SubGroupVM(TreeItemVMBase parent, SubGroup item)
            : base(parent, item)
        {
            this.Enabled = true;
        }
    }
}
