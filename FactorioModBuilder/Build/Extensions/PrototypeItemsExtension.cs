using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeItemsExtension : ExtensionBase<ItemData>
    {
        public PrototypeItemsExtension()
            : base(ExtensionType.PrototypeItems,
            ExtensionType.PrototypeEntities, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<ItemData> units, StreamWriter sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("data:extend(");
            sb.AppendLine("{");
            foreach(var i in units)
            {
                // verify the item contents
                if (!this.SubGroupNames.Contains(i.SubGroup))
                    return false;
                if (!this.EntityNames.Contains(i.PlaceResult))
                    return false;

                // write out the item
                sb.AppendLine("  {");
                sb.AppendLine("    type = \"item\",");
                sb.AppendLine("    name = \"" + i.Name +"\",");
                sb.AppendLine("    icon = \"" + i.Icon + "\",");
                sb.AppendLine("    flags = {}");
                sb.AppendLine("    subgroup = \"" + i.SubGroup + "\",");
                sb.AppendLine("    order = \"" + i.Order + "\",");
                sb.AppendLine("    place_result = \"" + i.PlaceResult + "\",");
                sb.AppendLine("    stack_size = \"" + i.StackSize + "\"");
                sb.AppendLine("  },");
            }
            if (sb.Length != 0)
                sb.Remove(sb.Length - 1, 1);
            sb.AppendLine("})");

            sw.Write(sb.ToString());
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "items.lua");
            return true;
        }
    }
}
