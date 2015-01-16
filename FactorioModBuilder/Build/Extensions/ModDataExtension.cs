using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModDataExtension : ExtensionBase<ModDataData>
    {
        public ModDataExtension()
            : base(ExtensionType.FactorioData)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModDataData> units, DirectoryInfo outDir)
        {
            try
            {
                string res;
                if(!this.BuildUnit(units, out res))
                {
                    this.Error("Failed to build data.lua");
                    return false;
                }

                using (var fs = File.Open(Path.Combine(outDir.FullName, "data.lua"), FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch (Exception e)
            {
                this.Fatal("Encountered exception creating data.lua: {0}", e.Message);
                return false;
            }

            return true;
        }

        protected override bool BuildUnit(IEnumerable<ModDataData> units, out string value)
        {
            value = " ";
            return true;
        }
    }
}
