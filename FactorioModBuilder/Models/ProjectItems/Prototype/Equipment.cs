﻿using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Equipment : TreeItem<Equipment>
    {
        public Equipment(string name) : base(name)
        {
        }
    }
}
