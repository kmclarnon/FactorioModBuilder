using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterItemVM : ProjectItem<GraphicsFilterItem, GraphicsFilterItemVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public string ExportPath
        {
            get { return _internal.ExportPath; }
            set { this.SetProperty(_internal, value); }
        }

        public string ImportPath
        {
            get { return _internal.ImportPath; }
            set { this.SetProperty(_internal, value); }
        }

        public GraphicsFilterItemVM(GraphicsFilterItem item)
            : this(null, item)
        {
        }

        public GraphicsFilterItemVM(TreeItemVMBase parent, GraphicsFilterItem item)
            : base(parent, item)
        {
        }
    }
}
