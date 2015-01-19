using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterVM : ProjectItem<GraphicsFilter, GraphicsFilterVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public GraphicsFilterVM(GraphicsFilter item)
            : base(item)
        {
        }

        public GraphicsFilterVM(GraphicsFilter item, IEnumerable<GraphicsFilterVM> children)
            : base(item, children)
        {
        }
    }
}
