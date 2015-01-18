using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeGroupsExtension : ExtensionBase<GroupBaseData>
    {
        public PrototypeGroupsExtension()
            : base(ExtensionType.PrototypeGroups, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<GroupBaseData> units, StreamWriter sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("data:extend(");
            sb.AppendLine("{");
            var sgd = units.Where(o => o.GetType() == typeof(SubGroupData)).Cast<SubGroupData>();
            var gd = units.Where(o => o.GetType() == typeof(GroupData)).Cast<GroupData>();

            // verify that each subgroup lists a valid group name
            foreach(var sg in sgd)
            {
                if (!gd.Where(o => o.Name == sg.Group).Any())
                {
                    this.Error("Unknown group: {0} specified in subgroup: {1}", sg.Group, sg.Name);
                    return false;
                }
            }

            foreach(var g in gd)
            {
                if (!this.GroupNames.Add(g.Name))
                {
                    this.Warning(WarningLevel.W1, "Skipping duplicate prototype definition for item-group: {0}", g.Name);
                    continue;
                }

                sb.AppendLine("  {");
                sb.AppendLine("    type = \"item-group\"");
                sb.AppendLine("    name = \"" + g.Name + "\",");
                sb.AppendLine("    icon = \"" + g.Icon + "\",");
                sb.AppendLine("    inventory_order = \"" + g.InvOrder + "\",");
                sb.AppendLine("    order = \"" + g.Order + "\"");
                sb.AppendLine("  },");

                foreach(var sg in sgd.Where(o => o.Group == g.Name))
                {
                    if(!this.SubGroupNames.Add(sg.Name))
                    {
                        this.Warning(WarningLevel.W1, "Skipping duplicate prototype definition for item-subgroup: {0}", g.Name);
                        continue;
                    }

                    sb.AppendLine("  {");
                    sb.AppendLine("    type = \"item-subgroup\",");
                    sb.AppendLine("    name = \"" + sg.Name + "\",");
                    sb.AppendLine("    group = \"" + sg.Group + "\",");
                    sb.AppendLine("    order = \"" + sg.Order + "\"");
                    sb.AppendLine("  },");
                }
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.AppendLine("})");

            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "item-groups.lua");
            return true;
        }
    }
}
