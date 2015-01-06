using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TileVM : TreeItemVM<Tile>
    {
        public TileVM(Tile tile)
            : base(tile)
        {
        }

        public TileVM(TreeItemVMBase parent, Tile tile)
            : base(parent, tile)
        {
        }
    }
}
