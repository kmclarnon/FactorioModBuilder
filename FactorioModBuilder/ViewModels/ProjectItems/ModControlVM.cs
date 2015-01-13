using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModControlVM : ProjectItem<ModControl, ModControlVM>
    {
        public override DataUnit CompilerData
        {
            get { return null; }
        }

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
