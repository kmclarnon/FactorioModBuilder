using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ItemsExtension : ExtensionBase<ItemData>
    {
        public ItemsExtension()
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
                // write out the item
                sb.AppendLine("  {");
                sb.AppendLine("    type = \"item\",");
                sb.AppendLine("    name = \"" + i.Name +"\",");
                sb.AppendLine("    icon = \"" + this.GraphicsPathLookup[i.Icon] + "\",");
                sb.AppendLine("    flags = {" + this.GetFlagString(i.Flag) + "},");
                sb.AppendLine("    subgroup = \"" + i.SubGroup + "\",");
                sb.AppendLine("    order = \"" + i.Order + "\",");
                if(i.PlaceResult != null && i.PlaceResult != String.Empty)
                    sb.AppendLine("    place_result = \"" + i.PlaceResult + "\",");
                sb.AppendLine("    stack_size = \"" + i.StackSize + "\"");
                sb.AppendLine("  },");
            }
            if (sb.Length != 0)
                sb.Remove(sb.Length - 1, 1);
            sb.AppendLine("})");

            string res = sb.ToString();
            sw.Write(res);
            return true;
        }

        protected override bool ValidateData(IEnumerable<ItemData> units)
        {
            foreach (var i in units)
            {
                if (!this.GraphicsPathLookup.ContainsKey(i.Icon))
                {
                    this.Error("Unknown image {0} specified in definition of the {1} item", i.Icon, i.Name);
                    return false;
                }

                if (!this.SubGroupNames.Contains(i.SubGroup))
                {
                    this.Error("Unknown subgroup {0} specified in the definition of the {1} item", i.SubGroup, i.Name);
                    return false;
                }

                if (i.PlaceResult != null && !this.EntityNames.Contains(i.PlaceResult))
                {
                    this.Error("Unknown place result {0} specified in the definition of the {1} item", i.PlaceResult, i.Name);
                    return false;
                }

                if (!this.ItemNames.Add(i.Name))
                {
                    this.Error("Detected duplicate item name {0}", i.Name);
                    return false;
                }
            }

            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "items.lua");
            return true;
        }

        private string GetFlagString(ItemFlag flag)
        {
            switch (flag)
            {
                case ItemFlag.GoesToQuickbar:
                    return "\"goes-to-quickbar\"";
                case ItemFlag.GoesToMainInventory:
                    return "\"goes-to-main-inventory\""; 
                case ItemFlag.GoesToQuickBarHidden:
                    return "\"goes-to-quickbar\", \"hidden\"";
                case ItemFlag.GoesToMainInventoryHidden:
                    return "\"goes-to-main-inventory\", \"hidden\"";
                default:
                    throw new Exception("Unknown Item flag: " + flag.ToString());
            }
        }
    }
}
