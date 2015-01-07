using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public abstract class CompilerMessage
    {
        public enum MessageType
        {
            Error,
            Warning,
            Info
        }

        public string Message { get; private set; }
        public MessageType Type { get; private set; }

        public CompilerMessage(MessageType type, string msg)
        {
            this.Type = type;
            this.Message = msg;
        }
    }
}
