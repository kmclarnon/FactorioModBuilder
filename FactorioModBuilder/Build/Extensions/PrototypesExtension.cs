using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypesExtension : ExtensionBase
    {
        public override bool BuildUnit(DataUnit unit)
        {
            throw new NotImplementedException();
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            var pd = unit as PrototypesData;
            if(pd == null)
            {
                this.Error("Expected intput to be prototypes data, recieved: {0}", unit.GetType().FullName);
                return false;
            }
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            throw new NotImplementedException();
        }
    }
}
