using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class DataLuaExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "data.lua"; }
        }

        public override bool BuildUnit(CompileUnit unit, DirectoryInfo result)
        {
            return true;
        }
    }
}
