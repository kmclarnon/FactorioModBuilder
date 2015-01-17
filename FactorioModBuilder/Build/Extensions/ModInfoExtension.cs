﻿using FactorioModBuilder.Build.Data;
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

            if (!Regex.IsMatch(info.Version, @"^\d{1,4}\.\d{1,4}\.\d{1,4}$"))
                return false;
            if (!(info.Name == this.ProjectName || info.Name + "_" + info.Version == this.ProjectName))
                return false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"name\" : \"" + info.Name + "\",");
            sb.AppendLine("  \"version\" : \"" + info.Version + "\",");
            sb.AppendLine("  \"title\" : \"" + info.Author + "\",");
            sb.AppendLine("  \"contact\" : \"" + info.Contact + "\",");
            sb.AppendLine("  \"homepage\" : \"" + info.Homepage + "\",");
            sb.AppendLine("  \"description\" : \"" + info.Description + "\",");
            sb.AppendLine("  \"dependencies\" : " + this.DependencyString);
            sb.AppendLine("}");

            sw.Write(sb.ToString());
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "info.json");
            return true;
        }
    }
}
