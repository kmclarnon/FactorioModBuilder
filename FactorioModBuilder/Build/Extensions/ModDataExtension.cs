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
            foreach (var f in this.GeneratedFiles)
                sb.AppendLine("require(\"" + this.GetRequirePath(f) + "\")");
            return true;
        }

        protected override bool ValidateData(IEnumerable<ModDataData> units)
        {
            return true;
        }

        private string GetRequirePath(string filePath)
        {
            var np = filePath.Substring(this.TemporaryDirectory.Length);
            return np;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "data.lua");
            return true;
        }
    }
}
