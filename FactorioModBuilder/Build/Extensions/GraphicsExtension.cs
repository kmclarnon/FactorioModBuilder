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
            try
            {
                string baseDir = "__" + this.ProjectName + "__";
                foreach (var g in units)
                {
                    if (!this.GraphicsPathLookup.ContainsKey(g.ImportPath))
                    {
                        FileInfo info = new FileInfo(g.ImportPath);
                        if (!info.Exists)
                            return false;

                        // modify the path and copy the image into the appropriate location
                        this.GraphicsPathLookup.Add(g.ImportPath, baseDir + "/" + g.ExportPath);

                        string dirPath = Path.Combine(this.TemporaryDirectory, Path.GetDirectoryName(g.ExportPath));
                        DirectoryInfo dinfo = new DirectoryInfo(dirPath);
                        if (!dinfo.Exists)
                            Directory.CreateDirectory(dirPath);

                        info.CopyTo(Path.Combine(dirPath, Path.GetFileName(g.ExportPath)), true);
                    }
                    else
                        return false;
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
