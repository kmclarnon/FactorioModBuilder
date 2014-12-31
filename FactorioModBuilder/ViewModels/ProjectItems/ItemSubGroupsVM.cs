using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ItemSubGroupsVM : ProjectItemVM
    {
        public ItemSubGroupsVM(ProjectItemVM parent, ItemSubGroups items)
            : base(parent, items)
        {
        }
    }
}
