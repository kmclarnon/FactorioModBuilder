using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsVM : ProjectItem<Graphics, GraphicsVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public GraphicsVM(Graphics item)
            : this(null, item)
        {
        }

        public GraphicsVM(TreeItemVMBase parent, Graphics item)
            : base(parent, item)
        {
        }
    }
}
