using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyVM : TreeItemVM<Technology>
    {
        public TechnologyVM(Technology tech)
            : base(tech)
        {
        }

        public TechnologyVM(TreeItemVMBase parent, Technology tech)
            : base(parent, tech)
        {
        }
    }
}
