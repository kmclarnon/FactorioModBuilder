﻿using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.Models.SolutionItems
{
    public class Solution : TreeItem<Solution>
    {
        public string Directory { get; set; }

        public Solution(string name, string dir)
            : base(name)
        {
            this.Directory = dir;
        }
    }
}
