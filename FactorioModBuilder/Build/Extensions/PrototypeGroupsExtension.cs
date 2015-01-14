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

        public override bool BuildUnit(IEnumerable<DataUnit> units, DirectoryInfo outDir)
        {
            StringBuilder groups = new StringBuilder();
            foreach(var u in units)
            {
                if(u is GroupsData)
                {
                    var gd = (GroupsData)u;
                    foreach (var su in gd.SubUnits)
                    {
                        var g = su as GroupData;
                        if (g == null)
                        {
                            this.Error("Expected subunit to be group data, recieved {0}", g.GetType().Name);
                            continue;
                        }

                        groups.AppendLine("  {");
                        groups.AppendLine("    type = " + "\"item-group\",");
                        groups.AppendLine("    name = " + "\"" + g.Name + "\",");
                        groups.AppendLine("    icon = " + "\"" + g.Icon + "\",");
                        groups.AppendLine("    inventory_order = " + "\"" + g.InvOrder + "\",");
                        groups.AppendLine("    order = " + "\"" + g.Order + "\"");
                        groups.AppendLine("  },");
                    }
                }
                else if(u is SubGroupsData)
                {
                    var gd = (SubGroupsData)u;
                    foreach (var su in gd.SubUnits)
                    {
                        var sg = su as SubGroupData;
                        if (sg == null)
                        {
                            this.Error("Expected subunit to be group data, recieved {0}", sg.GetType().Name);
                            continue;
                        }

                        groups.AppendLine("  {");
                        groups.AppendLine("    type = " + "\"item-subgroup\",");
                        groups.AppendLine("    name = " + "\"" + sg.Name + "\",");
                        groups.AppendLine("    group = " + "\"" + sg.Group + "\",");
                        groups.AppendLine("    order = " + "\"" + sg.Order + "\"");
                        groups.AppendLine("  },");
                    }
                }
                else
                {
                    this.Error("Encountered unexpected type building item-groups.lua: {0}", u.Type);
                }
            }

            string res = "data:extend(\n{\n" + groups.ToString(0, groups.Length - 1) + "})";

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
