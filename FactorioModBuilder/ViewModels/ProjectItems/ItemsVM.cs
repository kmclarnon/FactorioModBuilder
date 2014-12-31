using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ItemsVM : ProjectItemVM
    {
        public ItemsVM(ProjectItemVM parent, Items items)
            : base(parent, items)
        {
        }
    }
}
