using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipeVM : ProjectItemVM
    {
        public RecipeVM(ProjectItemVM parent, Recipe rec)
            : base(parent, rec)
        {
        }
    }
}
