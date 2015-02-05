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
    /// <summary>
    /// Prototypes view model
    /// </summary>
    public class PrototypesVM : ProjectItem<Prototypes, PrototypesVM>
    {
        /// <summary>
        /// Provides prototype data to the compiler at build time
        /// </summary>
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// All current group prototypes defined in this project
        /// </summary>
        public ObservableCollection<GroupVM> Groups
        {
            get
            {
                GroupsFilterVM gfvm;
                if (!this.TryFindElementDown(out gfvm))
                    throw new Exception("Could not find child: GroupsFilterVM");
                return gfvm.RecusiveTypedChildren;
            }
        }

        /// <summary>
        /// All current subgroup prototypes defined in this project
        /// </summary>
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

        /// <summary>
        /// All current entity prototypes defined in ths project
        /// </summary>
        public ObservableCollection<EntityVM> Entities
        {
            get
            {
                EntityFilterVM efvm;
                if (!this.TryFindElementDown(out efvm))
                    throw new Exception("Could not find child: EntityFilterVM");
                return efvm.Entities;
            }
        }

        /// <summary>
        /// All current graphics sources defined in this project
        /// </summary>
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
