using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModControlExtension : ExtensionBase
    {
        public ModControlExtension()
            : base(ExtensionType.FactorioControl)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            try
            {
                string res;
                if (!this.BuildUnit(unit, out res))
                {
                    this.Error("Failed to build control.lua");
                    return false;
                }

                using(var fs = File.Open(Path.Combine(outDir.FullName, "control.lua"), FileMode.Create))
                using(var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch(Exception e)
            {
                this.Fatal("Encountered exception creating control.lua: {0}", e.Message);
                return false;
            }

            return true;
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            value = " ";
            return true;
        }
    }
}
