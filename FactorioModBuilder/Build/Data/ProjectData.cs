using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class ProjectData : DataUnit
    {
        public string TempDir { get; set; }
        public string OutDir { get; set; }
        public string ProjectName { get; set; }
        public string BaseTempDirectory { get { return Path.Combine(TempDir, ProjectName); } }
        public string BaseOutDirectory { get { return Path.Combine(OutDir, ProjectName); } }

        public ProjectData(string name, string tempDir, string outDir)
            : base(Extensions.ExtensionType.Project)
        {
            this.TempDir = tempDir;
            this.OutDir = outDir;
            this.ProjectName = name;
        }
    

    }
}
