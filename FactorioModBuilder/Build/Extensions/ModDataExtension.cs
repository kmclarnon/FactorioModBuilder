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
            : base(ExtensionType.FactorioData)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModDataData> units, StreamWriter sw)
        {
            sw.Write(" ");
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
