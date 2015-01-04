﻿using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipesVM : ProjectItemVM<Recipes>
    {
        public RecipesVM(ProjectItemVMBase parent, Recipes rec)
            : base(parent, rec)
        {
        }
    }
}
