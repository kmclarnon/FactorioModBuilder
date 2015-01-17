using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModControlExtension : ExtensionBase<ModControlData>
    {
        public ModControlExtension()
            : base(ExtensionType.FactorioControl)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModControlData> units, StreamWriter sw)
        {
            sw.Write(" ");
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.TemporaryDirectory, "control.lua");
            return true;
        }
    }
}
