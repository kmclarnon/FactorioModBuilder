using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ControlLuaExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "control.lua"; }
        }

        public override bool BuildUnit(CompileUnit unit, DirectoryInfo outDir)
        {
            return true;
        }
    }
}
