using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyVM : ProjectItemVM<Technology>
    {
        public TechnologyVM(ProjectItemVMBase parent, Technology tech)
            : base(parent, tech)
        {
        }
    }
}
