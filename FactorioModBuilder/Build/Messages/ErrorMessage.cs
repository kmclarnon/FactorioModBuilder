using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public class ErrorMessage : CompilerMessage
    {
        public ErrorMessage(string msg)
            : base(MessageType.Error, msg)
        {
        }

        public ErrorMessage(string format, params object[] args)
            : base(MessageType.Error, format, args)
        {
        }
    }
}
