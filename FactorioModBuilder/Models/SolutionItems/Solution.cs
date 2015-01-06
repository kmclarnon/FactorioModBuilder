using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.Utility;
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

        public Solution(string name)
            : base(name)
        {
        }
    }
}
