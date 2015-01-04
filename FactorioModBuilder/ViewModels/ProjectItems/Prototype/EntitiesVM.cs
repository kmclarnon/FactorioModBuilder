using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EntitiesVM : ProjectItemVM<Entities>
    {
        public EntitiesVM(ProjectItemVMBase parent, Entities en)
            : base(parent, en)
        {
        }
    }
}
