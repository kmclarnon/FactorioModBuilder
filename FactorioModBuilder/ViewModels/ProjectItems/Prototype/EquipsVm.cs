using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipsVM : TreeItemVM<Equips>
    {
        public EquipsVM(Equips eq)
            : base(eq)
        {
        }

        public EquipsVM(TreeItemVMBase parent, Equips eq)
            : base(parent, eq)
        {
        }
    }
}
