using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TilesVM : ProjectItemVM<Tiles>
    {
        public TilesVM(ProjectItemVMBase parent, Tiles tiles)
            : base(parent, tiles)
        {
        }
    }
}
