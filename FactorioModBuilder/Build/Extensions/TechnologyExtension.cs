using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class TechnologyExtension : ExtensionBase<TechnologyData>
    {
        public TechnologyExtension()
            : base(ExtensionType.PrototypeTechnologies, 
            ExtensionType.Prototypes, ExtensionType.PrototypeRecipes)
        {
        }

        protected override bool BuildUnit(IEnumerable<TechnologyData> units, System.IO.StreamWriter sw)
        {
            sw.Write(" ");
            return true;
        }

        protected override bool ValidateData(IEnumerable<TechnologyData> units)
        {
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "technology.lua");
            return true;
        }
    }
}
