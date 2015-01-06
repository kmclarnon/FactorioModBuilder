using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModControlVM : TreeItemVM<ModControl>
    {
        public ModControlVM(ModControl control)
            : base(control)
        {
        }

        public ModControlVM(TreeItemVMBase parent, ModControl control)
            : base(parent, control)
        {
        }
    }
}
