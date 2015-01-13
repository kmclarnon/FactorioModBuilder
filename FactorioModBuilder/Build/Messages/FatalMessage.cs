using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public class FatalMessage : CompilerMessage
    {
        public FatalMessage(string msg)
            : base(MessageType.Fatal, msg)
        {
        }

        public FatalMessage(string format, params object[] args)
            : base(MessageType.Fatal, format, args)
        {
        }
    }
}
