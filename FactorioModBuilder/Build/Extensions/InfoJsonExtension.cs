﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class InfoJsonExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "info.json"; }
        }

        public override bool SeparateFile
        {
            get { return true; }
        }

        public override string Filename
        {
            get { return "info.json"; }
        }

        public override bool BuildUnit(CompileUnit unit, out string result)
        {
            result = String.Empty;
            if (unit.UType != CompileUnit.UnitType.Struct)
                return false;

            result = this.BuildTable(unit);

            return true;
        }
    }
}