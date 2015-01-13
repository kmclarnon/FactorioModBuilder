using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeSubGroupsExtension : ExtensionBase
    {
        public PrototypeSubGroupsExtension()
            : base(ExtensionType.PrototypeSubgroups)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            return true;
        }
    }
}
