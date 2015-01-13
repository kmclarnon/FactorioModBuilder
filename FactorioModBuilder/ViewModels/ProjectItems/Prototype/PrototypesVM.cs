using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class PrototypesVM : ProjectItem<Prototypes, PrototypesVM>
    {
        public override DataUnit CompilerData
        {
            get
            {
                GroupsVM gres;
                if (!this.TryFindElementDown<GroupsVM>(out gres))
                    throw new Exception("Could not find group child element");
                SubGroupsVM sgres;
                if (!this.TryFindElementDown<SubGroupsVM>(out sgres))
                    throw new Exception("Could not fond subgroup child element");
                return new PrototypesData((GroupsData)gres.CompilerData, 
                    (SubGroupsData)sgres.CompilerData);
            }
        }

        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                SubGroupsVM res;
                if (!this.TryFindElementDown<SubGroupsVM>(out res))
                    throw new Exception("Could not find subgroups child element");
                return res.ItemList;
            }
        }

        public ObservableCollection<GroupVM> PossibleGroups
        {
            get
            {
                GroupsVM res;
                if (!this.TryFindElementDown<GroupsVM>(out res))
                    throw new Exception("Could not find groups child element");
                return res.ItemList;
            }
        }

        public PrototypesVM(Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(types, children)
        {
        }

        public PrototypesVM(TreeItemVMBase parent, Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(parent, types, children)
        {
        }
    }
}
