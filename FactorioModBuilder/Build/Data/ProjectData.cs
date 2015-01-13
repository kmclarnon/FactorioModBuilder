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

        public ProjectData(string name, string tempDir, string outDir,
            ModInfoData info, ModControlData control, ModDataData data,
            PrototypesData prot, LocaleData loc)
            : base(Extensions.ExtensionType.Project)
        {
            this.TempDir = tempDir;
            this.OutDir = outDir;
            this.ProjectName = name;

            this.SubUnits.Add(info);
            this.SubUnits.Add(control);
            this.SubUnits.Add(data);
            this.SubUnits.Add(prot);
            this.SubUnits.Add(loc);
        }
    

    }
}
