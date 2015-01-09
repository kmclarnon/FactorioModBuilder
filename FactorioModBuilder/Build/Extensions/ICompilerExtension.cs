using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public interface ICompilerExtension
    {
        string SupportedUnitName { get; }
        bool SeparateFile { get; }
        string Filename { get; }
        Compiler Parent { get; set; }

        bool BuildUnit(CompileUnit unit, out string result);
    }
}
