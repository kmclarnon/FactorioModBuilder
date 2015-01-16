using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class GroupData : GroupBaseData
    {
        public string Icon { get; private set; }
        public string InvOrder { get; private set; }

        public GroupData(string name, string iconPath, string invOrder, string order)
        {
            this.Name = name;
            this.Icon = iconPath;
            this.InvOrder = InvOrder;
            this.Order = order;
        }
    }
}
