using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class GroupsExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "prototypes.groups"; }
        }

        public override bool BuildUnit(CompileUnit unit, System.IO.DirectoryInfo outDir)
        {
            if(unit.UType != CompileUnit.UnitType.List)
            {
                this.Parent.BuildMessages.Add(new ErrorMessage(
                    "Unexpected groups compile unit.  Type was: " + 
                    unit.UType + ", expected type was: " + CompileUnit.UnitType.List.ToString()));
                return false;
            }

            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "item-groups.lua"), FileMode.Create))
                using (var sr = new StreamWriter(fs))
                {
                    // wrap in data:extend factorio api call
                    sr.WriteLine("data:extend({");
                    var res = unit.ListValues.Select(o => this.BuildTable(o).TrimEnd(Environment.NewLine.ToArray()));
                    sr.WriteLine(String.Join("," + Environment.NewLine, res));
                    sr.WriteLine("})");
                }
            }
            catch(Exception e)
            {
                this.Parent.BuildMessages.Add(new ErrorMessage("Encountered an exception building groups: " + e.Message));
                return false;
            }

            return true;
        }
    }
}
