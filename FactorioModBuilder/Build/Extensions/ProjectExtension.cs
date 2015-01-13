using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ProjectExtension : ExtensionBase
    {
        public ProjectExtension() : base(ExtensionType.Project) { }

        public override bool BuildUnit(DataUnit unit)
        {
            var pd = unit as ProjectData;
            if (pd == null)
            {
                this.Error("Expected input to be project data, received: {0}", unit.GetType().Name);
                return false;
            }

            if (!this.PrepareProject(pd))
                return false;

            // process our actual project data
            foreach(var s in pd.SubUnits)
            {
                if (!this.CanContinue())
                    return false;

                ICompilerExtension ext;
                if(!this.TryGetCompilerExtension(s.Type, out ext))
                    this.Error("Could not find appropriate extension for: {0}", s.Type);
                else
                {
                    switch (s.Type)
                    {
                        case ExtensionType.FactorioInfo:
                            ext.BuildUnit(s, new DirectoryInfo(pd.BaseTempDirectory));
                            break;
                        case ExtensionType.FactorioControl:
                            break;
                        case ExtensionType.FactorioData:
                            break;
                        case ExtensionType.Prototypes:
                            break;
                        default:
                            this.Error("ProjectData contains an unsupported subunit: {0}", s.Type);
                            continue;
                    }
                }
            }

            // move the temporary directory project contents to the output directory
            if (Directory.Exists(pd.BaseOutDirectory))
                Directory.Delete(pd.BaseOutDirectory, true);
            Directory.Move(pd.BaseTempDirectory, pd.BaseOutDirectory);

            return true;
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            throw new NotImplementedException();
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            throw new NotImplementedException();
        }

        protected bool PrepareProject(ProjectData pd)
        {
            try
            {
                // get and validate our temporary directory
                var tmpDirInfo = new DirectoryInfo(pd.BaseTempDirectory);
                if (!tmpDirInfo.Exists)
                {
                    tmpDirInfo.Create();
                }
                else
                {
                    // clear out the temporary directory
                    foreach (var file in tmpDirInfo.GetFiles())
                        file.Delete();
                    foreach (var dir in tmpDirInfo.GetDirectories())
                        dir.Delete(true);
                }

                // get and validate our output directory
                var outDirInfo = new DirectoryInfo(pd.BaseOutDirectory);
                if (!outDirInfo.Exists)
                    outDirInfo.Create();
            }
            catch(Exception e)
            {
                this.Fatal("The compiler encountered exception preparing for project: {0}", e.Message);
                return false;
            }

            return true;
        }
    }
}
