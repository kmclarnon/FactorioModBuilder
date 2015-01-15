using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeGroupsExtension : ExtensionBase
    {
        public PrototypeGroupsExtension()
            : base(ExtensionType.PrototypeGroups)
        {
        }

        public override bool BuildUnit(IEnumerable<DataUnit> units, DirectoryInfo outDir)
        {
            

            return true;
        }
    }
}
