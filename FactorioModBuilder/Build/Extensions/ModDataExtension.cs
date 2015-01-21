using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModDataExtension : ExtensionBase<ModDataData>
    {
        public ModDataExtension()
            : base(ExtensionType.FactorioData, ExtensionType.PrototypeEntities, 
            ExtensionType.PrototypeEquipment, ExtensionType.PrototypeFluids,
            ExtensionType.PrototypeGroups, ExtensionType.PrototypeItems, 
            ExtensionType.PrototypeRecipes, ExtensionType.PrototypeTechnologies, 
            ExtensionType.PrototypeTiles, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModDataData> units, StreamWriter sw)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var f in this.GeneratedFiles.Where(o => o.StartsWith(this.TemporaryDirectory)))
            {
                var np = f.Substring(this.TemporaryDirectory.Length + 1).Replace('\\','.');
                if (np == "control.lua" || np == "data.lua" || np == "info.json")
                    continue;

                if (!np.Contains('.'))
                    continue;
                var ext = Path.GetExtension(f);
                np = np.Substring(0, np.Length - ext.Length);

                sb.AppendLine("require(\"" + np + "\")");
            }

            sw.Write(sb.ToString());
            return true;
        }

        protected override bool ValidateData(IEnumerable<ModDataData> units)
        {
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "data.lua");
            return true;
        }
    }
}
