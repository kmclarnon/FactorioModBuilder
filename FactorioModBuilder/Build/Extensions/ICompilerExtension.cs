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
        IEnumerable<ExtensionType> Dependencies { get; }
        void AttachToCompiler(Compiler c);
        bool BuildUnit(DataUnit unit);
        bool BuildUnit(DataUnit unit, DirectoryInfo outDir);
        bool BuildUnit(DataUnit unit, out string result);
        bool BuildUnit(IEnumerable<DataUnit> units);
        bool BuildUnit(IEnumerable<DataUnit> units, DirectoryInfo outDir);
        bool BuildUnit(IEnumerable<DataUnit> units, out string result);
    }
}
