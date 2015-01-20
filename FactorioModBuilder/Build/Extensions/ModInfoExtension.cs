using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModInfoExtension : ExtensionBase<ModInfoData>
    {
        public ModInfoExtension() 
            : base(ExtensionType.FactorioInfo, ExtensionType.FactorioDependencies) 
        { }

        protected override bool BuildUnit(IEnumerable<ModInfoData> units, StreamWriter sw)
        {
            var info = units.Single();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("{");
            sb.AppendLine("  \"name\" : \"" + info.Name + "\",");
            sb.AppendLine("  \"version\" : \"" + info.Version + "\",");
            sb.AppendLine("  \"title\" : \"" + info.Title + "\",");
            sb.AppendLine("  \"author\" : \"" + info.Author + "\",");
            sb.AppendLine("  \"contact\" : \"" + info.Contact + "\",");
            sb.AppendLine("  \"homepage\" : \"" + info.Homepage + "\",");
            sb.AppendLine("  \"description\" : \"" + info.Description + "\",");
            sb.AppendLine("  \"dependencies\" : " + this.DependencyString);
            sb.AppendLine("}");

            string res = sb.ToString();
            sw.Write(res);
            return true;
        }

        protected override bool ValidateData(IEnumerable<ModInfoData> units)
        {
            if (units.Count() != 1)
            {
                this.Error("Expected a single mod info data item, encountered multiple");
                return false;
            }
            var unit = units.Single();
            if (!Regex.IsMatch(unit.Version, @"^\d{1,4}\.\d{1,4}\.\d{1,4}$"))
            {
                this.Error("The mod version must be in the format Major.Middle.Minor and contain only numbers");
                return false;
            }
            if (!(unit.Name == this.ProjectName || unit.Name + "_" + unit.Version == this.ProjectName))
            {
                this.Error("The project name is not valid");
                return false;
            }
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "info.json");
            return true;
        }
    }
}
