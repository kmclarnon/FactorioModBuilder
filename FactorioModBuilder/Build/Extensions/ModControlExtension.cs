using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModControlExtension : ExtensionBase<ModControlData>
    {
        public ModControlExtension()
            : base(ExtensionType.FactorioControl)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModControlData> units, DirectoryInfo outDir)
        {
            try
            {
                string res;
                if (!this.BuildUnit(units, out res))
                {
                    this.Error("Failed to build control.lua");
                    return false;
                }

                using (var fs = File.Open(Path.Combine(outDir.FullName, "control.lua"), FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch (Exception e)
            {
                this.Fatal("Encountered exception creating control.lua: {0}", e.Message);
                return false;
            }

            return true;
        }

        protected override bool BuildUnit(IEnumerable<ModControlData> units, out string result)
        {
            result = " ";
            return true;
        }
    }
}
