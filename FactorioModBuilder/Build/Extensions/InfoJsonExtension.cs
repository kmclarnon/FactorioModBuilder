using FactorioModBuilder.Build.Messages;
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
            // create our file and wirte our data to it
            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "info.json"), FileMode.Create))
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    sr.Write(res);
                }
            }
            catch(Exception e)
            {
                Parent.BuildMessages.Add(new ErrorMessage(
                    "Encountered exception building info.json: " + e.Message));
                return false;
            }

            return true;
        }
    }
}
