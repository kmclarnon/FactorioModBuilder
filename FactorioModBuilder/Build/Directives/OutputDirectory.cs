using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Directives
{
    public class OutputDirectory : CompilerDirective
    {
        public OutputDirectory(string path)
            : base(DirectiveType.OutputDirectory, path)
        {
        }
    }
}
