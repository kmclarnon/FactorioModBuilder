using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class LocaleVM : ProjectItemVM<Locale>
    {
        public LocaleVM(ProjectItemVMBase parent, Locale loc) 
            : base(parent, loc)
        {
        }
    }
}
