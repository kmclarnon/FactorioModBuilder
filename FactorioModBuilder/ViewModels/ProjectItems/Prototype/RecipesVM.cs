using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipesVM : TreeItemVM<Recipes, RecipesVM>
    {
        public RecipesVM(Recipes rec)
            : base(rec)
        {
        }

        public RecipesVM(TreeItemVMBase parent, Recipes rec)
            : base(parent, rec)
        {
        }
    }
}
