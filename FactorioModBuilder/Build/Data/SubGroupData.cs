using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class SubGroupData : GroupBaseData
    {
        public string Group { get; private set; }

        public SubGroupData(string name, string group, string order)
        {
            this.Group = group;
            this.Order = order;
            this.Name = name;
        }
    }
}
