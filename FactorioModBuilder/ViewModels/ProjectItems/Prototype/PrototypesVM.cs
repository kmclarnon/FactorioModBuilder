using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using FactorioModBuilder.Resources.Icons;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class PrototypesVM : ProjectItem<Prototypes, PrototypesVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return null;
            }
        }

        public ObservableCollection<SubGroupVM> SubGroups
        {
            get
            {
                GroupsFilterVM gfvm;
                if (!this.TryFindElementDown(out gfvm))
                    throw new Exception("Could not find child: GroupsFilterVM");
                return gfvm.SubGroups;
            }
        }

        public ObservableCollection<IGraphicsSource> GraphicsSources { get; private set; }

        public PrototypesVM(Prototypes types, IEnumerable<TreeItemVMBase> children)
            : this(null, types, children)
        {
        }

        public PrototypesVM(TreeItemVMBase parent, Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(parent, types, children, DoubleClickBehavior.Ignore)
        {
            this.GraphicsSources = new ObservableCollection<IGraphicsSource>();
            this.Icon = AppIcon.Prototypes;
        }
    }
}
