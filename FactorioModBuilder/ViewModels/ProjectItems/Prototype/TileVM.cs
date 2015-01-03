using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TileVM : ProjectItemVM
    {
        public TileVM(ProjectItemVM parent, Tile tile)
            : base(parent, tile)
        {
        }
    }
}
