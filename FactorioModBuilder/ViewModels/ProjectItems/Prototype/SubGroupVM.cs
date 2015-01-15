using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupVM : ProjectItem<SubGroup, SubGroupVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new SubGroupData(this.Name, 
                    this.GroupName, this.Order).ToList();
            }
        }

        public string Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        public GroupVM Group
        {
            get { return this.GetProperty<GroupVM>(); }
            set { this.SetProperty(value, (() => this.GroupName = (value == null) ? String.Empty : value.Name)); }
        }

        public string GroupName
        {
            get { return _internal.Group; }
            set { this.SetProperty(_internal, value, false, null, "Group"); }
        }

        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
        }

        public bool Enabled
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
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

        public void ForceRemoveGroup()
        {
            this.SetProperty<Group>(null, null, false, "Group");
            this.GroupName = String.Empty;
        }
    }
}
