using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class SubGroupData : DataUnit
    {
        public string Group { get; private set; }
        public string Order { get; private set; }
        public string Name { get; private set; }

        public SubGroupData(string name, string group, string order)
            : base(Extensions.ExtensionType.PrototypeSubgroups)
        {
            this.Group = group;
            this.Order = order;
            this.Name = name;
        }
    }
}
