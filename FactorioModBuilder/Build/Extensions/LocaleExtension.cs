﻿using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class LocaleExtension : ExtensionBase<LocaleData>
    {
        public LocaleExtension()
            : base(ExtensionType.FactorioLocale)
        {
        }

        protected override bool BuildUnit(IEnumerable<LocaleData> units, StreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}
