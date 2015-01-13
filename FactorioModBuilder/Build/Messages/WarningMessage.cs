using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public class WarningMessage : CompilerMessage
    {
        public WarningLevel Level { get; private set; }

        public WarningMessage(WarningLevel level, string msg)
            : base(MessageType.Warning, msg)
        {
            this.Level = level;
        }

        public WarningMessage(WarningLevel level, string format, params object[] args)
            : base(MessageType.Warning, format, args)
        {
            this.Level = level;
        }
    }
}
