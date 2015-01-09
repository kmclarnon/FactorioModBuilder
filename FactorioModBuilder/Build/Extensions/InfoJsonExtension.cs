using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class InfoJsonExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "info.json"; }
        }

        public override bool BuildUnit(CompileUnit unit, DirectoryInfo outDir)
        {
            if (unit.UType != CompileUnit.UnitType.Struct)
                return false;

            var res = this.BuildTable(unit);
            // create our file
            using(var fs = File.Open(Path.Combine(outDir.FullName, "info.json"), FileMode.Create))
            using(StreamWriter sr = new StreamWriter(fs))
            {
                sr.Write(res);
            }

            return true;
        }
    }
}
