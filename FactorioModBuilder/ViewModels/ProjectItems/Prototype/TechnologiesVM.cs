using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologiesVM : TreeItemVM<Technologies, TechnologiesVM>
    {
        public TechnologiesVM(Technologies tech)
            : base(tech)
        {
        }

        public TechnologiesVM(TreeItemVMBase parent, Technologies tech)
            : base(parent, tech)
        {
        }
    }
}
