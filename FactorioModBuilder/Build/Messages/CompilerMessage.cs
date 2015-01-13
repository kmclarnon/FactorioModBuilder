using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public enum MessageType
    {
        Error,
        Warning,
        Info,
        Fatal
    }

    public enum WarningLevel
    {
        W1,
        W2,
        W3,
        WAll
    }

    public abstract class CompilerMessage
    {
        public string Message { get; private set; }
        public MessageType Type { get; private set; }

        public CompilerMessage(MessageType type, string msg)
        {
            this.Type = type;
            this.Message = msg;
        }

        public CompilerMessage(MessageType type, string format, params object[] args)
        {
            this.Type = type;
            this.Message = String.Format(format, args);
        }
    }
}
