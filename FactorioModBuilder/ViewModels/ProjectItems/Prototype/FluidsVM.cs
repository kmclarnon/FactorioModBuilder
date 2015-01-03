using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class FluidsVM : ProjectItemVM
    {
        public FluidsVM(ProjectItemVM parent, Fluids fl) 
            : base(parent, fl)
        {
        }
    }
}
