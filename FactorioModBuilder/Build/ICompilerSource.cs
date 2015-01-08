using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public interface ICompilerSource
    {
        CompileUnit CompilerData { get; }
        string CompilerKey { get; }
        IEnumerable<ICompilerSource> SubUnits { get; }
    }
}
