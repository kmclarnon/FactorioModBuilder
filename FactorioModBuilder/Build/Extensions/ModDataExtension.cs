using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModDataExtension : ExtensionBase<ModDataData>
    {
        public ModDataExtension()
            : base(ExtensionType.FactorioData)
        {
        }

        protected override bool BuildUnit(IEnumerable<ModDataData> units, StreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}
