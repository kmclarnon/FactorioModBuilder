using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public class GroupsData : DataUnit
    {
        public GroupsData(IEnumerable<GroupData> groups)
            : base(Extensions.ExtensionType.PrototypeGroups)
        {
            foreach (var g in groups)
                this.SubUnits.Add(g);
        }
    }
}
