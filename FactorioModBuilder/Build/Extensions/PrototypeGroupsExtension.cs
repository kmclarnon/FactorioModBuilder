using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeGroupsExtension : ExtensionBase
    {
        public PrototypeGroupsExtension()
            : base(ExtensionType.PrototypeGroups)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            var gd = unit as GroupsData;
            if(gd == null)
            {
                this.Error("Expected input to be groups data, received: {0}", unit.GetType().Name);
                return false;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var su in gd.SubUnits)
            {
                var g = su as GroupData;
                if (g == null)
                {
                    this.Error("Expected subunit to be group data, recieved {0}", g.GetType().Name);
                    continue;
                }

                sb.AppendLine("  {");
                sb.AppendLine("    type = " + "\"item-group\",");
                sb.AppendLine("    name = " + "\"" + g.Name + "\",");
                sb.AppendLine("    icon = " + "\"" + g.Icon + "\",");
                sb.AppendLine("    inventory_order = " + "\"" + g.InvOrder + "\",");
                sb.AppendLine("    order = " + "\"" + g.Order + "\"");
                sb.AppendLine("  },");
            }

            string res = "data:extend(\n{\n" + sb.ToString(0, sb.Length - 1) + "})";

            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "item-groups.lua"), FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch(Exception e)
            {
                this.Error("Encountered an exception creating item-groups.lua: {0}", e.Message);
                return false;
            }

            return true;
        }
    }
}
