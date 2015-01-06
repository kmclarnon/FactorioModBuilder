using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipeVM : TreeItemVM<Recipe>
    {
        public RecipeVM(Recipe rec)
            : base(rec)
        {
        }

        public RecipeVM(TreeItemVMBase parent, Recipe rec)
            : base(parent, rec)
        {
        }
    }
}
