using System;
using System.Collections.Generic;
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

        public override bool SeparateFile
        {
            get { return true; }
        }

        public override string Filename
        {
            get { return "data.lua"; }
        }

        public override bool BuildUnit(CompileUnit unit, out string result)
        {
            result = "";
            return false;
        }
    }
}
