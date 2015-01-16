using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeGroupsExtension : ExtensionBase<GroupBaseData>
    {
        public PrototypeGroupsExtension()
            : base(ExtensionType.PrototypeGroups)
        {
        }

        protected override bool BuildUnit(IEnumerable<GroupBaseData> units)
        {
            throw new NotImplementedException();
        }
    }
}
