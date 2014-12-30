﻿using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModControlVM : ProjectItemVM
    {
        private ModControl _mc { get { return (ModControl)_item; } }

        public ModControlVM(ProjectItemVM parent, ModControl control)
            : base(parent, control)
        {

        }
    }
}
