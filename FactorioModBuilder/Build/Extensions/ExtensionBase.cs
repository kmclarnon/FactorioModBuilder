using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public abstract class ExtensionBase : ICompilerExtension
    {
        public abstract ExtensionType Extension { get; }

        public Compiler Parent { get; set; }

        public ExtensionBase()
        {
        }

        public abstract bool BuildUnit(CompileUnit unit, DirectoryInfo outDir);

    }
}
