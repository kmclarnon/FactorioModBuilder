using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class GraphicsExtension : ExtensionBase<GraphicsData>
    {
        public GraphicsExtension()
            : base(ExtensionType.FactorioGraphics, ExtensionType.Project)
        {
        }

        protected override bool BuildUnit(IEnumerable<GraphicsData> units, StreamWriter sr)
        {
            string baseDir = "__" + this.ProjectName + "__";
            foreach(var g in units)
                this.GraphicsPathLookup.Add(g.ImportPath, baseDir + "/" + g.ExportPath);
            return true;
        }
    }
}
