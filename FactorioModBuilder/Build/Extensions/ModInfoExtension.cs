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
            var unit = units.Single();
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "info.json");
            return true;
        }
    }
}
