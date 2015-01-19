using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class GraphicsFilterItem : TreeItem<GraphicsFilterItem>
    {
        public string ImportPath { get; set; }
        public string ExportPath { get; set; }

        public GraphicsFilterItem(string name)
            : base(name)
        {
        }
    }
}
