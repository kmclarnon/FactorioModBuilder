using FactorioModBuilder.Build;
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
        public override CompileUnit CompilerData
        {
            get
            {
                Dictionary<string, CompileUnit> res = new Dictionary<string, CompileUnit>();
                GroupsVM gres;
                if(!this.TryFindElementDown<GroupsVM>(out gres))
                    throw new Exception("Could not find groups child element");
                res.Add(gres.CompilerKey, gres.CompilerData);
                SubGroupsVM sgres;
                if(!this.TryFindElementDown<SubGroupsVM>(out sgres))
                    throw new Exception("Could not find subgroups child element");
                res.Add(sgres.CompilerKey, sgres.CompilerData);
                return new CompileUnit(res);
            }
        }

        public override string CompilerKey
        {
            get { return "prototypes"; }
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
