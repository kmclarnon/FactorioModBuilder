using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class SubGroupsVM : ProjectItemVM
    {
        public SubGroupsVM(ProjectItemVM parent, SubGroups items)
            : base(parent, items)
        {
        }
    }
}
