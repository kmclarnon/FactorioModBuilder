using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public class SubGroupData : DataUnit
    {
        [DataMember]
        public string type { get; private set; }
        [DataMember]
        public string group { get; private set; }
        [DataMember]
        public string order { get; private set; }
        
        public SubGroupData(string type, string group, string order)
            : base(Extensions.ExtensionType.PrototypeSubgroups)
        {
            this.type = type;
            this.group = group;
            this.order = order;
        }
    }
}
