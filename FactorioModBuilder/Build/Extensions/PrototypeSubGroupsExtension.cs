using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeSubGroupsExtension : ExtensionBase
    {
        public PrototypeSubGroupsExtension()
            : base(ExtensionType.PrototypeSubgroups)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            var gd = unit as SubGroupsData;
            if (gd == null)
            {
                this.Error("Expected input to be subgroups data, received: {0}", unit.GetType().Name);
                return false;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var su in gd.SubUnits)
            {
                var g = su as SubGroupData;
                if (g == null)
                {
                    this.Error("Expected subunit to be subgroup data, recieved {0}", g.GetType().Name);
                    continue;
                }

                sb.AppendLine("  {");
                sb.AppendLine("    type = " + "\" item-subgroup\",");
                sb.AppendLine("    name = " + "\"" + g.Name + "\",");
                sb.AppendLine("    group = " + "\"" + g.Group + "\",");
                sb.AppendLine("    order = " + "\"" + g.Order + "\"");
                sb.AppendLine("  },");
            }

            string res = "data:extend(\n{\n" + sb.ToString(0, sb.Length - 1) + "})";

            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "item-subgroups.lua"), FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch (Exception e)
            {
                this.Error("Encountered an exception creating item-subgroups.lua: {0}", e.Message);
                return false;
            }

            return true;
        }
    }
}
