using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class PrototypesVM : ProjectItemVM
    {
        public PrototypesVM(ProjectItemVM parent, Prototypes types)
            : base(parent, types)
        {
        }
    }
}
