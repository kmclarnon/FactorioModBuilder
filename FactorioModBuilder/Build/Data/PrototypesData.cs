using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class PrototypesData : DataUnit
    {
        public PrototypesData(GroupsData groups, SubGroupsData subgroups)
            : base(Extensions.ExtensionType.Prototypes)
        {
            this.SubUnits.Add(groups);
            this.SubUnits.Add(subgroups);
        }
    }
}
