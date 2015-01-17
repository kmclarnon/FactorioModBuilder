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
    public class ProjectExtension : ExtensionBase<ProjectData>
    {
        public ProjectExtension() : base(ExtensionType.Project) { }

        protected override bool BuildUnit(IEnumerable<ProjectData> units, StreamWriter sw)
        {
            var pd = units.Single();

            this.Info("Build Started: Project: {0}", pd.ProjectName);

            if (!this.PrepareProject(pd))
                return false;

            return true;
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
