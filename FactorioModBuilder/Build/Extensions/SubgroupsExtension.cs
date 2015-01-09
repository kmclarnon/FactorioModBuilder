using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class SubgroupsExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "prototypes.subgroups"; }
        }

        public override bool BuildUnit(CompileUnit unit, System.IO.DirectoryInfo outDir)
        {
            if (unit.UType != CompileUnit.UnitType.List)
            {
                this.Parent.BuildMessages.Add(new ErrorMessage(
                    "Unexpected subgroups compile unit.  Type was: " +
                    unit.UType + ", expected type was: " + CompileUnit.UnitType.List.ToString()));
                return false;
            }

            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "item-subgroups.lua"), FileMode.Create))
                using (var sr = new StreamWriter(fs))
                {
                    // wrap in data:extend factorio api call
                    sr.WriteLine("data:extend({");
                    StringBuilder sb = new StringBuilder();
                    var res = unit.ListValues.Select(o => this.BuildTable(o).TrimEnd(Environment.NewLine.ToArray()));
                    sr.WriteLine(String.Join("," + Environment.NewLine, res));
                    sr.WriteLine("})");
                }
            }
            catch (Exception e)
            {
                this.Parent.BuildMessages.Add(new ErrorMessage("Encountered an exception building subgroups: " + e.Message));
                return false;
            }

            return true;
        }
    }
}
