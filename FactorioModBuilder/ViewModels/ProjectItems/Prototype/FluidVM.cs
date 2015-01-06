using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class FluidVM : TreeItemVM<Fluid>
    {
        public FluidVM(Fluid fluid)
            : base(fluid)
        {
        }

        public FluidVM(TreeItemVMBase parent, Fluid fluid)
            : base(parent, fluid)
        {
        }
    }
}
