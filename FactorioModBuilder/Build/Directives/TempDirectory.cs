using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Directives
{
    public class TempDirectory : CompilerDirective
    {
        public TempDirectory(string path)
            : base(DirectiveType.TemporaryDirectory, path)
        {
        }
    }
}
