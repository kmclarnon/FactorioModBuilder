using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypesExtension : ExtensionBase
    {
        public PrototypesExtension()
            : base(ExtensionType.Prototypes)
        {
        }

        public override bool BuildUnit(IEnumerable<DataUnit> unit, DirectoryInfo outDir)
        {
            

            return true;
        }

        public override bool BuildUnit(IEnumerable<DataUnit> unit, out string value)
        {
            throw new NotImplementedException();
        }
    }
}
