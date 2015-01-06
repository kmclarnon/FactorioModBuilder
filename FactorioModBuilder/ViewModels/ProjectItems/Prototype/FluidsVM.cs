﻿using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class FluidsVM : TreeItemVM<Fluids>
    {
        public FluidsVM(TreeItemVMBase parent, Fluids fl) 
            : base(parent, fl)
        {
        }
    }
}
