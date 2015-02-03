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

        public ObservableCollection<IGraphicsSource> GraphicsSources { get; private set; }

        public PrototypesVM(Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(types, children, DoubleClickBehavior.Ignore)
        {
            this.GraphicsSources = new ObservableCollection<IGraphicsSource>();
            this.Icon = AppIcon.Prototypes;
        }

        public PrototypesVM(TreeItemVMBase parent, Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(parent, types, children, DoubleClickBehavior.Ignore)
        {
            this.GraphicsSources = new ObservableCollection<IGraphicsSource>();
            this.Icon = AppIcon.Prototypes;
        }
    }
}
