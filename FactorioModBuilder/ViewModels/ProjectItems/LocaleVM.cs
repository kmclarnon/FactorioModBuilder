﻿using FactorioModBuilder.Build;
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
    public class LocaleVM : ProjectItem<Locale, LocaleVM>
    {
        public override DataUnit CompilerData
        {
            get { return new LocaleData(); }
        }

        public LocaleVM(Locale loc) 
            : base(loc)
        {
        }

        public LocaleVM(TreeItemVMBase parent, Locale loc)
            : base(parent, loc)
        {
        }
    }
}
