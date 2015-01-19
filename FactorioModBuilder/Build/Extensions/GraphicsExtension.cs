using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            return true;
        }
    }
}
