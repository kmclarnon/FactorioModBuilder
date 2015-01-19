using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class GraphicsData : DataUnit
    {
        public string SourceName { get; private set; }
        public string ImportPath { get; private set; }
        public string ExportPath { get; private set; }

        public GraphicsData(string sourceName, string importPath, string exportPath)
            : base(Extensions.ExtensionType.FactorioGraphics)
        {
            this.SourceName = sourceName;
            this.ImportPath = importPath;
            this.ExportPath = exportPath;
        }
    }
}
