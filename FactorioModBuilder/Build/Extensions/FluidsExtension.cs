using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class FluidsExtension : ExtensionBase<FluidData>
    {
        public FluidsExtension()
            : base(ExtensionType.PrototypeFluids, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<FluidData> units, System.IO.StreamWriter sr)
        {
            sr.Write(" ");
            return true;
        }

        protected override bool ValidateData(IEnumerable<FluidData> units)
        {
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "fluids.lua");
            return true;
        }
    }
}
