using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TilesVM : TreeItemVM<Tiles, TilesVM>
    {
        public TilesVM(Tiles tiles)
            : base(tiles)
        {
        }

        public TilesVM(TreeItemVMBase parent, Tiles tiles)
            : base(parent, tiles)
        {
        }
    }
}
