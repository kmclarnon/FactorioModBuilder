using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModDependenciesExtension : ExtensionBase<ModInfoDependencyData>
    {
        public ModDependenciesExtension()
            : base(ExtensionType.FactorioDependencies)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModInfoDependencyData> units, System.IO.StreamWriter sr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if(units != null && units.Any())
                sb.Append(units.Select(o => this.GetDepString(o)).Aggregate((a, b) => a + ", " + b));
            sb.Append("]");
            this.DependencyString = sb.ToString();
            return true;
        }

        private string GetDepString(ModInfoDependencyData dep)
        {
            string result = String.Empty;
            if (dep.Optional)
                result += "? ";
            result += dep.Name + " " + dep.Version;
            return "\"" + result + "\"";
        }
    }
}
