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
        bool BuildUnit(IEnumerable<DataUnit> units);
    }
}
