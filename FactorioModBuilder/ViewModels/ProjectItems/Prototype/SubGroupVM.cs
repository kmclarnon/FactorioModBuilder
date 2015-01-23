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
                    this.GroupName, this.Order).ListWrap();
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
            set { this.SetProperty(value, null, (() => this.GroupName = (value == null) ? String.Empty : value.Name)); }
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

        public SubGroupVM(SubGroup item)
            : this(null, item)
        {
        }

        public SubGroupVM(TreeItemVMBase parent, SubGroup item)
            : base(parent, item)
        {
        }

        public void ForceRemoveGroup()
        {
            this.SetProperty<GroupVM>(null, null, null, true, "Group");
            this.GroupName = String.Empty;
        }
    }
}
