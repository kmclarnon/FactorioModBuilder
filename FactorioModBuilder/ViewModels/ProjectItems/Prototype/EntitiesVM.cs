using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EntitiesVM : TreeItemVM<Entities>
    {
        public EntitiesVM(Entities en)
            : base(en)
        {
        }

        public EntitiesVM(TreeItemVMBase parent, Entities en)
            : base(parent, en)
        {
        }
    }
}
