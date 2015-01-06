using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class LocaleVM : TreeItemVM<Locale>
    {
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
