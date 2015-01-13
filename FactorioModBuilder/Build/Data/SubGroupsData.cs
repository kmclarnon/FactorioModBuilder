using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class SubGroupsData : DataUnit
    {
        public SubGroupsData(IEnumerable<SubGroupData> subgroups)
            : base(Extensions.ExtensionType.PrototypeSubgroups)
        {
            foreach (var g in subgroups)
                this.SubUnits.Add(g);
        }
    }
}
