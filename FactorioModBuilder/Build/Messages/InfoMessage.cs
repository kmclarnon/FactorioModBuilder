﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Messages
{
    public class InfoMessage : CompilerMessage
    {
        public InfoMessage(string msg)
            : base(MessageType.Info, msg)
        {
        }
    }
}
