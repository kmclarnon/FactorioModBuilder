﻿using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Filter : TreeItem<Filter>
    {
        public Filter(string name)
            : base(name)
        {
        }
    }
}
