using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeRecipeExtension : ExtensionBase<RecipeData>
    {
        public PrototypeRecipeExtension() : base(ExtensionType.PrototypeRecipes, 
            ExtensionType.Prototypes, ExtensionType.PrototypeItems)
        {
        }

        protected override bool BuildUnit(IEnumerable<RecipeData> units, System.IO.StreamWriter sr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("data:extend(");
            sb.AppendLine("{");
            foreach (var r in units)
            {
                sb.AppendLine("  {");
                sb.AppendLine("    type = \"recipe\",");
                sb.AppendLine("    name = \"" + r.Name + "\",");
                sb.AppendLine("    enabled = \"" + r.Enabled + "\",");
                if(r.EnergyReq != 0)
                    sb.AppendLine("    energy_required = \"" + r.EnergyReq + "\",");
                sb.AppendLine("    ingredients = ");
                sb.AppendLine("    {");
                foreach (var i in r.Ingredients)
                    sb.AppendLine("      {\"" + i.Item1 + "\"," + i.Item2 + "},");
                sb.Length -= (Environment.NewLine.Length + 1);
                sb.AppendLine();
                sb.AppendLine("    },");
                if (r.ResultCount != 1)
                {
                    sb.AppendLine("    result = \"" + r.Result + "\",");
                    sb.AppendLine("    result_count = " + r.ResultCount);
                }
                else
                {
                    sb.AppendLine("    result = \"" + r.Result + "\",");
                }
                sb.AppendLine("  },");
            }
            if(sb.Length > 2)
                sb.Length = sb.Length - (Environment.NewLine.Length + 1);
            sb.AppendLine("");
            sb.AppendLine("})");
            var res = sb.ToString();
            sr.Write(res);

            return true;
        }

        protected override bool ValidateData(IEnumerable<RecipeData> units)
        {
            foreach(var r in units)
            {
                if (r.Ingredients == null || r.Ingredients.Count < 1)
                    return false;
                if (!this.ItemNames.Contains(r.Result))
                    return false;
                if (r.EnergyReq < 0)
                    return false;
                foreach (var i in r.Ingredients)
                    if (!this.ItemNames.Contains(i.Item1))
                        return false;
            }

            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "recipes.lua");
            return true;
        }
    }
}
