using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModControlVM : ProjectItemVM
    {
        public ModControlVM(ProjectItemVM parent, ModControl control)
            : base(parent, control)
        {

        }
    }
}
