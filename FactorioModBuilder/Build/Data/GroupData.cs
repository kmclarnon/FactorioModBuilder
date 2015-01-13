using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public class GroupData : DataUnit
    {
        [DataMember]
        public string type { get; private set; }
        [DataMember]
        public string icon { get; private set; }
        [DataMember]
        public string invorder { get; private set; }
        [DataMember]
        public string order { get; private set; }

        public GroupData(string type, string iconPath, string invOrder, string order)
            : base(Extensions.ExtensionType.PrototypeGroups)
        {
            this.type = type;
            this.icon = iconPath;
            this.invorder = invorder;
            this.order = order;
        }
    }
}
