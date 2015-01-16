using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypesExtension : ExtensionBase<PrototypeData>
    {
        public PrototypesExtension()
            : base(ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<PrototypeData> units)
        {
            throw new NotImplementedException();
        }
    }
}
