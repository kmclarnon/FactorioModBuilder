using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class GroupBaseData : DataUnit
    {
        public string Order { get; protected set; }
        public string Name { get; protected set; }

        public GroupBaseData()
            : base(Extensions.ExtensionType.PrototypeGroups)
        {
        }
    }
}
