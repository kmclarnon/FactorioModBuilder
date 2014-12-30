using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ItemGroupsVM : ProjectItemVM
    {
        public ItemGroupsVM(ProjectItemVM parent, ItemGroups groups)
            : base(parent, groups)
        { 
        }
    }
}
