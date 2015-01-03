using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TilesVM : ProjectItemVM
    {
        public TilesVM(ProjectItemVM parent, Tiles tiles)
            : base(parent, tiles)
        {
        }
    }
}
