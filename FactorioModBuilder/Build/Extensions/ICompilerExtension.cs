using FactorioModBuilder.Build.Data;
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
        ExtensionType Extension { get; }
        Compiler Parent { get; set; }
        bool BuildUnit(DataUnit unit, DirectoryInfo outDir);
    }
}
