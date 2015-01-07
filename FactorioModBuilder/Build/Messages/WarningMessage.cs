using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public class WarningMessage : CompilerMessage
    {
        public WarningMessage(string msg)
            : base(MessageType.Warning, msg)
        {
        }
    }
}
