using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public interface ICompilerExtension
    {
        string SupportedUnitName { get; }
        Compiler Parent { get; set; }
        bool BuildUnit(CompileUnit unit, DirectoryInfo outDir);
    }
}
