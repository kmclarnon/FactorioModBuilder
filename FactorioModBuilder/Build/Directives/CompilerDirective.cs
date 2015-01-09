using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Directives
{
    public abstract class CompilerDirective
    {
        public enum DirectiveType
        {
            TemporaryDirectory,
            OutputDirectory,
            ProjectName
        }

        public DirectiveType Type { get; private set; }
        public string Data { get; private set; }

        public CompilerDirective(DirectiveType type, string data)
        {
            this.Type = type;
            this.Data = data;
        }
    }
}
