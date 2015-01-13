using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public interface ICompilerSource
    {
        DataUnit CompilerData { get; }
    }
}
