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
using WpfUtils.Extensions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupVM : ProjectItem<SubGroup, SubGroupVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new SubGroupData(this.Name, 
                    this.Group, this.Order).ListWrap();
            }
        }

        public string Type
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public GroupVM GroupItem
        {
            get { return this.GetProperty<GroupVM>(); }
            set { this.SetProperty(value, false, null, (x => this.Group = (x == null) ? String.Empty : x.Name)); }
        }

        public string Group
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Order
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public SubGroupVM(SubGroup item)
            : this(null, item)
        {
        }

        public SubGroupVM(TreeItemVMBase parent, SubGroup item)
            : base(parent, item, DoubleClickBehavior.OpenParent)
        {
        }

        public void ForceRemoveGroup()
        {
            this.SetProperty(null, (() => this.Group));
            this.Group = String.Empty;
        }
    }
}
