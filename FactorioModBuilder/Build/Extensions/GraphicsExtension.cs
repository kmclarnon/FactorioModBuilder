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

        protected override bool BuildUnit(IEnumerable<GraphicsData> units, StringBuilder sb)
        {
            try
            {
                string baseDir = "__" + this.ProjectName + "__";
                foreach (var g in units)
                {
                    // modify the path and copy the image into the appropriate location
                    this.GraphicsPathLookup.Add(g.ImportPath, baseDir + "/" + g.ExportPath);

                    string dirPath = Path.Combine(this.TemporaryDirectory, Path.GetDirectoryName(g.ExportPath));
                    DirectoryInfo dinfo = new DirectoryInfo(dirPath);
                    if (!dinfo.Exists)
                        Directory.CreateDirectory(dirPath);

                    FileInfo info = new FileInfo(g.ImportPath);
                    info.CopyTo(Path.Combine(dirPath, Path.GetFileName(g.ExportPath)), true);
                }

                return true;
            }
            catch(Exception e)
            {
                this.Error("Encountered exception processing graphics data: {0}", e.Message);
                return false;
            }
        }

        protected override bool ValidateData(IEnumerable<GraphicsData> units)
        {
            foreach(var g in units)
            {
                if (this.GraphicsPathLookup.ContainsKey(g.ImportPath))
                {
                    this.Error("Duplicate import path detected in the graphics data");
                    return false;
                }
                FileInfo info = new FileInfo(g.ImportPath);
                if (!info.Exists)
                {
                    this.Error("Graphics data contains an invalid import path: {0}", g.ImportPath);
                    return false;
                }
            }

            return true;
        }
    }
}
